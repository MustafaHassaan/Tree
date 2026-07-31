using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tradeflow.Domain.Entities;

namespace Tradeflow.Infrastructure.Configurations;

public class EmployeeWarehouseConfiguration : IEntityTypeConfiguration<EmployeeWarehouse>
{
    public void Configure(EntityTypeBuilder<EmployeeWarehouse> builder)
    {
        builder.HasKey(eh => new { eh.EmployeeId, eh.WarehouseId });
        
        builder.HasOne(eh => eh.Employee)
            .WithMany(e => e.EmployeeWarehouses)
            .HasForeignKey(eh => eh.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(eh => eh.Warehouse)
            .WithMany(w => w.EmployeeWarehouses)
            .HasForeignKey(eh => eh.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
