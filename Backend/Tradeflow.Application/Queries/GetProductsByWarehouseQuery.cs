using MediatR;

namespace Tradeflow.Application.Queries;

public record GetProductsByWarehouseQuery : IRequest<List<ProductStockDto>>
{
    public int WarehouseId { get; init; }
}

public record ProductStockDto
{
    public int ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public string Barcode { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}
