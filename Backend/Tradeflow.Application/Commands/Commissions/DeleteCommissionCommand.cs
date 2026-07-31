using MediatR;

namespace Tradeflow.Application.Commands.Commissions;

public record DeleteCommissionCommand : IRequest<bool>
{
    public int Id { get; init; }
}
