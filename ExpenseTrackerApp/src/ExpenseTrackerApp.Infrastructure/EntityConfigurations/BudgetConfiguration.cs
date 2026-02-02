using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTrackerApp.Infrastructure.EntityConfigurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budget");

        builder.HasKey(b => b.BudgetId);

        builder.Property(b => b.Amount)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(b => b.StartDate)
            .IsRequired();
        
        builder.Property(b => b.EndDate)
            .IsRequired();
        
        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
