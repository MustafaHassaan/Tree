using MediatR;

namespace Tradeflow.Application.Commands.Products;

public record SoftDeleteProductCommand : IRequest<bool>
{
    public int Id { get; init; }
}
