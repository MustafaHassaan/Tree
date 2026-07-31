using MediatR;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDetailDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var order = await orderRepo.GetByIdAsync(request.OrderId);

        if (order == null)
            return null;

        var customerRepo = _unitOfWork.Repository<Customer>();
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();
        var productRepo = _unitOfWork.Repository<Product>();

        var customer = await customerRepo.GetByIdAsync(order.CustomerId);
        var salesRep = await employeeRepo.GetByIdAsync(order.SalesRepId);
        var warehouse = await warehouseRepo.GetByIdAsync(order.WarehouseId);

        var orderDetails = await _unitOfWork.Repository<OrderDetail>()
            .GetAllAsync();

        var orderItems = orderDetails.Where(od => od.OrderId == order.Id).ToList();

        var itemDtos = new List<OrderItemDetailDto>();
        foreach (var item in orderItems)
        {
            var product = await productRepo.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                itemDtos.Add(new OrderItemDetailDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductBarcode = product.Barcode,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                });
            }
        }

        return new OrderDetailDto
        {
            OrderId = order.Id,
            CustomerName = customer?.Name ?? "Unknown",
            SalesRepName = salesRep?.Name ?? "Unknown",
            WarehouseName = warehouse?.Name ?? "Unknown",
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt,
            Items = itemDtos
        };
    }
}
