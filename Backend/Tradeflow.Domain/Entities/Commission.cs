namespace Tradeflow.Domain.Entities;

public class Commission
{
    public int Id { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal Percentage { get; set; }
    public string? Notes { get; set; }
    
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
