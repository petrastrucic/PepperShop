using AutoMapper;
using HealthChecks.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PepperShop.Cart.API.Settings;
using PepperShop.Cart.API.Utilities;
using PepperShop.Cart.Data.Repositories;
using PepperShop.Cart.Library;
using PepperShop.Cart.Library.Services;
using Serilog;
using System.Security.Claims;

namespace PepperShop.Cart.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting application");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext());

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CartServiceScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "cartService");
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Database layer configuration
            builder.Services.Configure<DatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));

            var dbConfig = builder.Configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
            CosmosClient cosmosClient = DatabaseInitiator.ConfigureDbClient(dbConfig);

            builder.Services.AddSingleton(cosmosClient);
            builder.Services.AddScoped<ICartRepository<Data.Entities.Cart>>(sp =>
                new CartRepository(
                    sp.GetRequiredService<CosmosClient>(),
                    dbConfig.DatabaseName,
                    dbConfig.ContainerName
                )
            );

            await DatabaseInitiator.ConfigureDatabaseAsync(cosmosClient, dbConfig);
            #endregion

            #region Business layer configuration
            // Add AutoMapper configuration
            builder.Services.AddSingleton(provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                }, loggerFactory);

                config.AssertConfigurationIsValid();

                return config.CreateMapper();
            });

            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddAzureCosmosDB(
                    optionsFactory: sp => new AzureCosmosDbHealthCheckOptions
                    {
                        DatabaseId = dbConfig.DatabaseName,
                        ContainerIds = new[] { dbConfig.ContainerName }
                    },
                    name: "cosmosdb",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "database", "cosmosdb" });
            #endregion

            var app = builder.Build();

            app.UseSerilogRequestLogging();
            app.MapHealthChecks("/health/self");
            app.MapHealthChecks("/health/cosmosdb");

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
                .RequireAuthorization("CartServiceScope");

            app.MapControllers();

            app.Run();
        }
    }
}
