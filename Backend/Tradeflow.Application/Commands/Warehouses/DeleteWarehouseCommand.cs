using MediatR;

namespace Tradeflow.Application.Commands.Warehouses;

public record DeleteWarehouseCommand : IRequest<bool>
{
    public int Id { get; init; }
}
