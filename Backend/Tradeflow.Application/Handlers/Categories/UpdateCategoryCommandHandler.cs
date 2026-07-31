using MediatR;
using Tradeflow.Application.Commands.Categories;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Categories;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepo = _unitOfWork.Repository<Category>();
        var category = await categoryRepo.GetByIdAsync(request.Id);

        if (category == null)
            throw new InvalidOperationException($"Category {request.Id} not found");

        category.Name = request.Name;
        await categoryRepo.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
