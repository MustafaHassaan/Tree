using MediatR;
using Tradeflow.Application.Commands.Employees;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Employees;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var orderRepo = _unitOfWork.Repository<Order>();
        var employeeWarehouseRepo = _unitOfWork.Repository<EmployeeWarehouse>();
        var employee = await employeeRepo.GetByIdAsync(request.Id);

        if (employee == null)
            throw new InvalidOperationException($"Employee {request.Id} not found");

        // Check if employee has sales orders
        var allOrders = await orderRepo.GetAllAsync();
        var hasOrders = allOrders.Any(o => o.SalesRepId == request.Id);

        if (hasOrders)
            throw new InvalidOperationException($"Cannot delete employee {request.Id} because it has associated sales orders. Please delete the orders first.");

        // Check if employee has warehouse assignments
        var allEmployeeWarehouses = await employeeWarehouseRepo.GetAllAsync();
        var hasWarehouses = allEmployeeWarehouses.Any(ew => ew.EmployeeId == request.Id);

        if (hasWarehouses)
            throw new InvalidOperationException($"Cannot delete employee {request.Id} because it has warehouse assignments. Please unassign the employee from warehouses first.");

        await employeeRepo.DeleteAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
