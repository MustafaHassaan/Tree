using MediatR;
using Tradeflow.Application.Commands.Products;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productRepo = _unitOfWork.Repository<Product>();
        var product = await productRepo.GetByIdAsync(request.Id);

        if (product == null)
            throw new InvalidOperationException($"Product {request.Id} not found");

        product.Name = request.Name;
        product.Barcode = request.Barcode;
        product.Price = request.Price;
        product.Cost = request.Cost;
        product.CategoryId = request.CategoryId;

        await productRepo.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
