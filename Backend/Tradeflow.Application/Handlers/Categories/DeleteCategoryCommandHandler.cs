using MediatR;
using Tradeflow.Application.Commands.Categories;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Categories;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryRepo = _unitOfWork.Repository<Category>();
        var productRepo = _unitOfWork.Repository<Product>();
        var category = await categoryRepo.GetByIdAsync(request.Id);

        if (category == null)
            throw new InvalidOperationException($"Category {request.Id} not found");

        // Check if category has products
        var allProducts = await productRepo.GetAllAsync();
        var hasProducts = allProducts.Any(p => p.CategoryId == request.Id);

        if (hasProducts)
            throw new InvalidOperationException($"Cannot delete category {request.Id} because it has associated products. Please delete or reassign the products first.");

        await categoryRepo.DeleteAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
