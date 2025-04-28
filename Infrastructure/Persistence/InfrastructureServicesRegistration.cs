using Microsoft.Extensions.Configuration;
using Persistence.Data.Identity;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                var redisConnectionString = Configuration.GetConnectionString("RedisConnectionString");
                if (string.IsNullOrEmpty(redisConnectionString))
                {
                    throw new ArgumentNullException(nameof(redisConnectionString), "Redis connection string cannot be null or empty.");
                }
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            return Services;
        }
    }
}
