using CalorieCounter.Domain.AggregatesModels;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.Contexts
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
