using MediatR;

namespace Tradeflow.Application.Commands.Employees;

public record DeleteEmployeeCommand : IRequest<bool>
{
    public int Id { get; init; }
}
