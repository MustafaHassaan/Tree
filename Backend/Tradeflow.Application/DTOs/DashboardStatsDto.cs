namespace Tradeflow.Application.DTOs;

public class DashboardStatsDto
{
    public TotalSalesStats TotalSales { get; set; } = null!;
    public List<DailySalesData> DailySales { get; set; } = new();
    public List<WarehouseStats> WarehouseStats { get; set; } = new();
    public List<TopProductStats> TopProducts { get; set; } = new();
    public List<SalesRepStats> SalesReps { get; set; } = new();
    public int LowStockAlerts { get; set; }
}

public class TotalSalesStats
{
    public decimal TotalSales { get; set; }
    public decimal MonthlyRevenue { get; set; }
    public decimal SalesChangePercentage { get; set; }
    public decimal RevenueChangePercentage { get; set; }
}

public class DailySalesData
{
    public string Day { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public decimal Actual { get; set; }
}

public class WarehouseStats
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int CurrentStock { get; set; }
    public decimal Percentage { get; set; }
}

public class TopProductStats
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int TotalSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public string Trend { get; set; } = string.Empty;
}

public class SalesRepStats
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal AchievementPercentage { get; set; }
    public int Rank { get; set; }
}
