using MediatR;

namespace Tradeflow.Application.Commands.Warehouses;

public record UpdateWarehouseCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}
