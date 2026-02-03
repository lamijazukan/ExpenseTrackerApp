using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTrackerApp.Infrastructure.EntityConfigurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expense");

        builder.HasKey(e => e.ExpenseId);
        
        builder.HasOne(e => e.Transaction)
            .WithMany()
            .HasForeignKey(e => e.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Amount)
            .HasPrecision(12, 2)
            .IsRequired();
        
        builder.Property(e => e.ProductName)
            .HasMaxLength(150)
            .IsRequired();
       
    }
}
