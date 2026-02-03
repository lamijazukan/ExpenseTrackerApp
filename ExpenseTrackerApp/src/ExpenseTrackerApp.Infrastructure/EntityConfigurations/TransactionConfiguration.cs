using ExpenseTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTrackerApp.Infrastructure.EntityConfigurations;

public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        
        builder.HasKey(t => t.TransactionId);
        
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.PaidDate)
            .IsRequired();
        
        builder.Property(t => t.Store)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(t => t.TotalAmount)
            .HasPrecision(12, 2)
            .IsRequired();
        
        builder.Property(t => t.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("NOW()");
    }
    
}