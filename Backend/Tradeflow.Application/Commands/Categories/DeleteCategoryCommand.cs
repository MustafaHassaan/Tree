using MediatR;

namespace Tradeflow.Application.Commands.Categories;

public record DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; init; }
}
