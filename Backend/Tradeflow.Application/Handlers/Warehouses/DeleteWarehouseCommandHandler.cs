using MediatR;
using Tradeflow.Application.Commands.Warehouses;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Warehouses;

public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();
        var orderRepo = _unitOfWork.Repository<Order>();
        var stockRepo = _unitOfWork.Repository<Stock>();
        var employeeWarehouseRepo = _unitOfWork.Repository<EmployeeWarehouse>();
        var warehouse = await warehouseRepo.GetByIdAsync(request.Id);

        if (warehouse == null)
            throw new InvalidOperationException($"Warehouse {request.Id} not found");

        // Check if warehouse has orders
        var allOrders = await orderRepo.GetAllAsync();
        var hasOrders = allOrders.Any(o => o.WarehouseId == request.Id);

        if (hasOrders)
            throw new InvalidOperationException($"Cannot delete warehouse {request.Id} because it has associated orders. Please delete the orders first.");

        // Check if warehouse has stock records
        var allStocks = await stockRepo.GetAllAsync();
        var hasStocks = allStocks.Any(s => s.WarehouseId == request.Id);

        if (hasStocks)
            throw new InvalidOperationException($"Cannot delete warehouse {request.Id} because it has associated stock records. Please delete the stock records first.");

        // Check if warehouse has assigned employees
        var allEmployeeWarehouses = await employeeWarehouseRepo.GetAllAsync();
        var hasEmployees = allEmployeeWarehouses.Any(ew => ew.WarehouseId == request.Id);

        if (hasEmployees)
            throw new InvalidOperationException($"Cannot delete warehouse {request.Id} because it has assigned employees. Please unassign the employees first.");

        await warehouseRepo.DeleteAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
