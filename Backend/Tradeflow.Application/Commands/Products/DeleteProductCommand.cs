using MediatR;

namespace Tradeflow.Application.Commands.Products;

public record DeleteProductCommand : IRequest<bool>
{
    public int Id { get; init; }
}
