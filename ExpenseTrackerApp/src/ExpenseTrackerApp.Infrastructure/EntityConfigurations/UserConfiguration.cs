using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpenseTrackerApp.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasDefaultValueSql("gen_random_uuid()"); // PostgreSQL UUID default

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            builder.OwnsOne(u => u.Preferences, prefs =>
            {
                prefs.ToJson();

                prefs.Property(p => p.Language)
                    .HasConversion<string>();

                prefs.Property(p => p.Currency)
                    .HasConversion<string>();
            });
        }
    }
}