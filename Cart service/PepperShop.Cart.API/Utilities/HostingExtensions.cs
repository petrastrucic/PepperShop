namespace PepperShop.Cart.API.Utilities
{
    internal static class HostingExtensions
    {
        //public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddAuthentication()
        //        .AddJwtBearer(options =>
        //        {
        //            options.Authority = "https://localhost:5001";
        //            options.TokenValidationParameters.ValidateAudience = false;
        //        });

        //    builder.Services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("CartServiceScope", policy =>
        //        {
        //            policy.RequireAuthenticatedUser();
        //            policy.RequireClaim("scope", "cartService");
        //        });
        //    });
        //    builder.Services.AddControllers();
        //    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //    builder.Services.AddOpenApi();
        //    builder.Services.AddEndpointsApiExplorer();
        //    builder.Services.AddSwaggerGen();

        //    builder.Services.Configure<DatabaseSettings>(
        //        builder.Configuration.GetSection("DatabaseSettings"));

        //    return builder.Build();
        //}
    }
}
