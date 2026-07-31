using Tradeflow.Domain.Enums;

namespace Tradeflow.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CustomerType Type { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
