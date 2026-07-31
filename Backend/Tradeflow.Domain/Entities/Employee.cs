using Tradeflow.Domain.Enums;

namespace Tradeflow.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int? CommissionId { get; set; }
    
    public Commission? Commission { get; set; }
    public ICollection<EmployeeWarehouse> EmployeeWarehouses { get; set; } = new List<EmployeeWarehouse>();
    public ICollection<Order> SalesOrders { get; set; } = new List<Order>();
}
