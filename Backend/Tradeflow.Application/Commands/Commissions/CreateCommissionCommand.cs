using MediatR;

namespace Tradeflow.Application.Commands.Commissions;

public record CreateCommissionCommand : IRequest<int>
{
    public decimal TargetAmount { get; init; }
    public decimal Percentage { get; init; }
    public string? Notes { get; init; }
}
