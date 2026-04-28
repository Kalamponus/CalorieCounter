using CalorieCounter.Domain.AggregatesModels;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly string _connectionString;

        public UserContext(DbContextOptions<UserContext> options, IHostApplicationBuilder builder) : base(options)
        {
            _connectionString = builder.Configuration.GetConnectionString("MainDB") ?? throw new InvalidOperationException("Connection string 'MainDB' not found.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
