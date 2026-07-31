using MediatR;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Commands.Customers;

public record CreateCustomerCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public CustomerType Type { get; init; }
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}
