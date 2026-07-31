using Tradeflow.Domain.Enums;

namespace Tradeflow.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int SalesRepId { get; set; }
    public int WarehouseId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Customer Customer { get; set; } = null!;
    public Employee SalesRep { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
