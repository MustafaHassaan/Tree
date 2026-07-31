using MediatR;

namespace Tradeflow.Application.Commands;

public record AssignEmployeeToWarehouseCommand : IRequest<bool>
{
    public int EmployeeId { get; init; }
    public int WarehouseId { get; init; }
}
