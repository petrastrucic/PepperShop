namespace PepperShop.Cart.API.Utilities
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
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

            return builder.Build();
        }

        //public static WebApplication ConfigurePipeline(this WebApplication app)
        //{
        //    _ = app.UseSerilogRequestLogging();

        //    if (app.Environment.IsDevelopment())
        //    {
        //        _ = app.UseDeveloperExceptionPage();
        //    }

        //    return app;
        //}

        // TODO: add logging
        //public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
        //{
        //    // Set up logging to write regular entries to console, and diagnostics data to a file.
        //    // See https://duende.link/diagnostics
        //    _ = builder.Services.AddSerilog(lc =>
        //    {
        //        _ = lc.WriteTo.Logger(consoleLogger =>
        //        {
        //            _ = consoleLogger.WriteTo.Console(
        //                outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
        //                formatProvider: CultureInfo.InvariantCulture);
        //            if (builder.Environment.IsDevelopment())
        //            {
        //                _ = consoleLogger.Filter.ByExcluding(Matching.FromSource("Duende.IdentityServer.Diagnostics.Summary"));
        //            }
        //        });
        //        if (builder.Environment.IsDevelopment())
        //        {
        //            _ = lc.WriteTo.Logger(fileLogger =>
        //            {
        //                _ = fileLogger
        //                    .WriteTo.File("./diagnostics/diagnostic.log", rollingInterval: RollingInterval.Day,
        //                        fileSizeLimitBytes: 1024 * 1024 * 10, // 10 MB
        //                        rollOnFileSizeLimit: true,
        //                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
        //                        formatProvider: CultureInfo.InvariantCulture)
        //                    .Filter
        //                    .ByIncludingOnly(Matching.FromSource("Duende.IdentityServer.Diagnostics.Summary"));
        //            }).Enrich.FromLogContext().ReadFrom.Configuration(builder.Configuration);
        //        }
        //    });
        //    return builder;
        //}
    }
}
