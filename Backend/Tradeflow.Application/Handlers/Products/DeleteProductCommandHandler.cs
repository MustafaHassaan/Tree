using MediatR;
using Tradeflow.Application.Commands.Products;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Products;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productRepo = _unitOfWork.Repository<Product>();
        var orderDetailRepo = _unitOfWork.Repository<OrderDetail>();
        var stockRepo = _unitOfWork.Repository<Stock>();
        var product = await productRepo.GetByIdAsync(request.Id);

        if (product == null)
            throw new InvalidOperationException($"Product {request.Id} not found");

        // Check if product has order details
        var allOrderDetails = await orderDetailRepo.GetAllAsync();
        var hasOrderDetails = allOrderDetails.Any(od => od.ProductId == request.Id);

        if (hasOrderDetails)
            throw new InvalidOperationException($"Cannot delete product {request.Id} because it has associated order details. Please delete the orders first.");

        // Check if product has stock records
        var allStocks = await stockRepo.GetAllAsync();
        var hasStocks = allStocks.Any(s => s.ProductId == request.Id);

        if (hasStocks)
            throw new InvalidOperationException($"Cannot delete product {request.Id} because it has associated stock records. Please delete the stock records first.");

        await productRepo.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
