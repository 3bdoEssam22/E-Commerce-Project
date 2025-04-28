using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
