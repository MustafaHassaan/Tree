using MediatR;
using Tradeflow.Application.Commands;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var stockRepo = _unitOfWork.Repository<Stock>();
        var productRepo = _unitOfWork.Repository<Product>();

        // Validate stock availability
        foreach (var item in request.Items)
        {
            var availableStock = await stockRepo.GetAllAsync();
            var stockForProduct = availableStock
                .FirstOrDefault(s => s.ProductId == item.ProductId && s.WarehouseId == request.WarehouseId);

            if (stockForProduct == null || stockForProduct.Quantity < item.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product {item.ProductId} in warehouse {request.WarehouseId}");
            }
        }

        // Create order
        var order = new Order
        {
            CustomerId = request.CustomerId,
            SalesRepId = request.SalesRepId,
            WarehouseId = request.WarehouseId,
            Status = OrderStatus.Pending,
            TotalAmount = 0,
            CreatedAt = DateTime.UtcNow
        };

        await orderRepo.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        // Create order details and deduct stock
        foreach (var item in request.Items)
        {
            var product = await productRepo.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product {item.ProductId} not found");

            var unitPrice = product.Price;
            var totalPrice = unitPrice * item.Quantity;

            var orderDetail = new OrderDetail
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice
            };

            await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetail);

            // Deduct stock
            var stock = (await stockRepo.GetAllAsync())
                .First(s => s.ProductId == item.ProductId && s.WarehouseId == request.WarehouseId);
            stock.Quantity -= item.Quantity;
            await stockRepo.UpdateAsync(stock);

            order.TotalAmount += totalPrice;
        }

        await orderRepo.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return order.Id;
    }
}
