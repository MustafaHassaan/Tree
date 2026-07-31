using MediatR;

namespace Tradeflow.Application.Commands.Customers;

public record DeleteCustomerCommand : IRequest<bool>
{
    public int Id { get; init; }
}
