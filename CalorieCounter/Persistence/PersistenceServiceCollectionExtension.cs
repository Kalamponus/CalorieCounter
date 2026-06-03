using CalorieCounter.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Persistence
{
    public static class PersistenceServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
