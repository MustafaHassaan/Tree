using MediatR;
using Tradeflow.Application.Commands.Products;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Products;

public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(SoftDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productRepo = _unitOfWork.Repository<Product>();
        var product = await productRepo.GetByIdAsync(request.Id);

        if (product == null)
            throw new InvalidOperationException($"Product {request.Id} not found");

        product.IsDeleted = true;
        await productRepo.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
