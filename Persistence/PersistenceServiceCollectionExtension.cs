using CalorieCounter.Infrastructure.Contexts;

namespace CalorieCounter.Persistence
{
    public static class PersistenceServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<UserContext>();
            return services;
        }
    }
}
