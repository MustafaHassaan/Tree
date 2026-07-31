using MediatR;

namespace Tradeflow.Application.Queries;

public record GetSalesRepPerformanceQuery : IRequest<SalesRepPerformanceDetailDto>
{
    public int SalesRepId { get; init; }
}

public record SalesRepPerformanceDetailDto
{
    public int SalesRepId { get; init; }
    public string SalesRepName { get; init; } = string.Empty;
    public decimal? TargetAmount { get; init; }
    public decimal? CommissionPercentage { get; init; }
    public decimal TotalSales { get; init; }
    public decimal AchievementPercentage { get; init; }
    public decimal EarnedCommission { get; init; }
    public int TotalOrders { get; init; }
}
