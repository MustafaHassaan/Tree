using MediatR;

namespace Tradeflow.Application.Commands.Commissions;

public record UpdateCommissionCommand : IRequest<bool>
{
    public int Id { get; init; }
    public decimal TargetAmount { get; init; }
    public decimal Percentage { get; init; }
    public string? Notes { get; init; }
}
