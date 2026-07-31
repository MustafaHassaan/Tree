using MediatR;

namespace Tradeflow.Application.Commands.Warehouses;

public record CreateWarehouseCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}
