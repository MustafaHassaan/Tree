using MediatR;
using Tradeflow.Application.Commands;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class AssignEmployeeToWarehouseCommandHandler : IRequestHandler<AssignEmployeeToWarehouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignEmployeeToWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignEmployeeToWarehouseCommand request, CancellationToken cancellationToken)
    {
        var employeeWarehouseRepo = _unitOfWork.Repository<EmployeeWarehouse>();
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();

        var employee = await employeeRepo.GetByIdAsync(request.EmployeeId);
        if (employee == null)
            throw new InvalidOperationException($"Employee {request.EmployeeId} not found");

        var warehouse = await warehouseRepo.GetByIdAsync(request.WarehouseId);
        if (warehouse == null)
            throw new InvalidOperationException($"Warehouse {request.WarehouseId} not found");

        // Check if assignment already exists
        var existingAssignments = await employeeWarehouseRepo.GetAllAsync();
        var existing = existingAssignments
            .FirstOrDefault(ea => ea.EmployeeId == request.EmployeeId && ea.WarehouseId == request.WarehouseId);

        if (existing != null)
            return true; // Already assigned

        var employeeWarehouse = new EmployeeWarehouse
        {
            EmployeeId = request.EmployeeId,
            WarehouseId = request.WarehouseId
        };

        await employeeWarehouseRepo.AddAsync(employeeWarehouse);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
