using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpenseTrackerApp.Infrastructure.EntityConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfile");

            builder.HasKey(u => u.UserProfileId);
            
            
            builder.HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<UserProfile>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(500)
                .IsRequired(false);
            
            builder.Property(u => u.Language)
                .IsRequired()
                .HasConversion<string>();   
            
            builder.Property(u => u.Currency)
                .IsRequired()
                .HasConversion<string>();
          
        }
    }
}