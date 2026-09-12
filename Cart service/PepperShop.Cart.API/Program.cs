using Microsoft.Extensions.Options;
using PepperShop.Cart.API.Settings;
using PepperShop.Cart.API.Utilities;
using System.Security.Claims;

namespace PepperShop.Cart.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder
                .ConfigureServices();

            await DatabaseInitiator.ConfigureDatabaseAsync(app.Services.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            // Configure the HTTP request pipeline.
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
