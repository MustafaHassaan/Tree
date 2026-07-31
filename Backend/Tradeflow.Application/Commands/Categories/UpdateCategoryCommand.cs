using MediatR;

namespace Tradeflow.Application.Commands.Categories;

public record UpdateCategoryCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
