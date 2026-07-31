using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradeflow.Domain.Entities;

namespace Tradeflow.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasOne(e => e.Commission)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CommissionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(e => e.SalesOrders)
            .WithOne(o => o.SalesRep)
            .HasForeignKey(o => o.SalesRepId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
