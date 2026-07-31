using MediatR;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class GetOrdersBySalesRepQueryHandler : IRequestHandler<GetOrdersBySalesRepQuery, List<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersBySalesRepQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersBySalesRepQuery request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var orderDetailRepo = _unitOfWork.Repository<OrderDetail>();
        var customerRepo = _unitOfWork.Repository<Customer>();
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();

        var orders = await orderRepo.GetAllAsync();
        var customers = await customerRepo.GetAllAsync();
        var warehouses = await warehouseRepo.GetAllAsync();
        var orderDetails = await orderDetailRepo.GetAllAsync();

        var salesRepOrders = orders.Where(o => o.SalesRepId == request.SalesRepId).ToList();

        var result = new List<OrderDto>();

        foreach (var order in salesRepOrders)
        {
            var customer = customers.FirstOrDefault(c => c.Id == order.CustomerId);
            var warehouse = warehouses.FirstOrDefault(w => w.Id == order.WarehouseId);
            var items = orderDetails
                .Where(od => od.OrderId == order.Id)
                .Select(od => new OrderItemDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity
                })
                .ToList();

            result.Add(new OrderDto
            {
                OrderId = order.Id,
                CustomerName = customer?.Name ?? "Unknown",
                WarehouseName = warehouse?.Name ?? "Unknown",
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                Items = items
            });
        }

        return result;
    }
}
