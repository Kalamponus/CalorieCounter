using CalorieCounter.Domain.AggregatesModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieCounter.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Age)
                .IsRequired();
            builder.Property(u => u.Gender)
                .IsRequired();
            builder.Property(u => u.Weight)
                .IsRequired();
            builder.Property(u => u.Height)
                .IsRequired();
            builder.Property(u => u.PhysicalActivityLevel)
                .IsRequired();
        }
    }
}
