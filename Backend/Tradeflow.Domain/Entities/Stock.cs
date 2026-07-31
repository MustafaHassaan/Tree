using Tradeflow.Domain.Entities;

namespace Tradeflow.Domain.Entities;

public class Stock
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    
    public Product? Product { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
}
