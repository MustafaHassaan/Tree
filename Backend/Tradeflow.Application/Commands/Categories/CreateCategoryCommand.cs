using MediatR;

namespace Tradeflow.Application.Commands.Categories;

public record CreateCategoryCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
}
