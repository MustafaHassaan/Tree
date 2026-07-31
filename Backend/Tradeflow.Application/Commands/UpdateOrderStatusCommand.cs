using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Commands;

public record UpdateOrderStatusCommand : IRequest<bool>
{
    public int OrderId { get; init; }
    public OrderStatus Status { get; init; }
}
