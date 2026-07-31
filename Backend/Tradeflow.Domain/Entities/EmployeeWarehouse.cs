namespace Tradeflow.Domain.Entities;

public class EmployeeWarehouse
{
    public int EmployeeId { get; set; }
    public int WarehouseId { get; set; }
    
    public Employee Employee { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
}
