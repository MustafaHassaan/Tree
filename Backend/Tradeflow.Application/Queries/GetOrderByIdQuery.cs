using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Queries;

public record GetOrderByIdQuery : IRequest<OrderDetailDto?>
{
    public int OrderId { get; init; }
}

public record OrderDetailDto
{
    public int OrderId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string SalesRepName { get; init; } = string.Empty;
    public string WarehouseName { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<OrderItemDetailDto> Items { get; init; } = new();
}

public record OrderItemDetailDto
{
    public int ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public string ProductBarcode { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
}
