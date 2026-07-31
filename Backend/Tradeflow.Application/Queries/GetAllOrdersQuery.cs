using MediatR;

namespace Tradeflow.Application.Queries;

public record GetAllOrdersQuery : IRequest<List<OrderDto>>
{
}
