using MediatR;

namespace Tradeflow.Application.Queries;

public record GetDashboardStatsQuery : IRequest<DashboardStatsDto>
{
}

public record DashboardStatsDto
{
    public decimal TotalSales { get; init; }
    public decimal MonthlyRevenue { get; init; }
    public int ActiveCustomers { get; init; }
    public int LowStockAlerts { get; init; }
    public List<DailySalesDto> DailySales { get; init; } = new();
    public List<WarehouseStatusDto> WarehouseStatus { get; init; } = new();
    public List<TopProductDto> TopProducts { get; init; } = new();
    public List<SalesRepPerformanceDto> SalesRepsPerformance { get; init; } = new();
}

public record DailySalesDto
{
    public string Day { get; init; } = string.Empty;
    public decimal Total { get; init; }
    public decimal Actual { get; init; }
}

public record WarehouseStatusDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal CapacityPercentage { get; init; }
    public string Color { get; init; } = string.Empty;
}

public record TopProductDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public int Sold { get; init; }
    public decimal Revenue { get; init; }
    public string Trend { get; init; } = string.Empty;
    public string TrendClass { get; init; } = string.Empty;
}

public record SalesRepPerformanceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Position { get; init; } = string.Empty;
    public decimal Sales { get; init; }
    public string Goal { get; init; } = string.Empty;
    public int Rank { get; init; }
    public bool Active { get; init; }
}