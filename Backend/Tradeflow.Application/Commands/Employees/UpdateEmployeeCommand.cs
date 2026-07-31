using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Commands.Employees;

public record UpdateEmployeeCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public EmployeeRole Role { get; init; }
    public string Phone { get; init; } = string.Empty;
    public string? Password { get; init; }
    public int? CommissionId { get; init; }
}
