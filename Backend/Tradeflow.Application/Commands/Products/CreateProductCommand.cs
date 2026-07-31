using MediatR;

namespace Tradeflow.Application.Commands.Products;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string Barcode { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal Cost { get; init; }
    public int CategoryId { get; init; }
}
