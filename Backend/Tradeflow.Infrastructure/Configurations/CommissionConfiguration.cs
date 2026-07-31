using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradeflow.Domain.Entities;

namespace Tradeflow.Infrastructure.Configurations;

public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
{
    public void Configure(EntityTypeBuilder<Commission> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.TargetAmount)
            .HasPrecision(12, 2)
            .IsRequired();
        
        builder.Property(c => c.Percentage)
            .HasPrecision(5, 2)
            .IsRequired();
        
        builder.Property(c => c.Notes)
            .HasMaxLength(500);
        
        builder.HasMany(c => c.Employees)
            .WithOne(e => e.Commission)
            .HasForeignKey(e => e.CommissionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
