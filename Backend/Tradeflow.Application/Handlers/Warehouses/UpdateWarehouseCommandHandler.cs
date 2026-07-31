using MediatR;
using Tradeflow.Application.Commands.Warehouses;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Warehouses;

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouseRepo = _unitOfWork.Repository<Warehouse>();
        var warehouse = await warehouseRepo.GetByIdAsync(request.Id);

        if (warehouse == null)
            throw new InvalidOperationException($"Warehouse {request.Id} not found");

        warehouse.Name = request.Name;
        warehouse.Location = request.Location;

        await warehouseRepo.UpdateAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
