namespace Tradeflow.Domain.Entities;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    public ICollection<EmployeeWarehouse> EmployeeWarehouses { get; set; } = new List<EmployeeWarehouse>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
