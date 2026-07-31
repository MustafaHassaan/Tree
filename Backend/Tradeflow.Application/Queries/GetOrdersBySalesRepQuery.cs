using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Queries;

public record GetOrdersBySalesRepQuery : IRequest<List<OrderDto>>
{
    public int SalesRepId { get; init; }
}

public record OrderDto
{
    public int OrderId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string WarehouseName { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<OrderItemDto> Items { get; init; } = new();
}

public record OrderItemDto
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
