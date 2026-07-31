using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Commands;

public record CreateOrderCommand : IRequest<int>
{
    public int CustomerId { get; init; }
    public int SalesRepId { get; init; }
    public int WarehouseId { get; init; }
    public List<CreateOrderItemDto> Items { get; init; } = new();
}

public record CreateOrderItemDto
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
