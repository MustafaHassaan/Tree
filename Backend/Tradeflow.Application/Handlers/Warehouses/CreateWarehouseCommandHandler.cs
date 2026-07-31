using MediatR;
using Tradeflow.Application.Commands.Warehouses;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Warehouses;

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateWarehouseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse
        {
            Name = request.Name,
            Location = request.Location
        };

        await _unitOfWork.Repository<Warehouse>().AddAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        return warehouse.Id;
    }
}
