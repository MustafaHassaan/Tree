using MediatR;
using Tradeflow.Application.Commands.Commissions;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Commissions;

public class DeleteCommissionCommandHandler : IRequestHandler<DeleteCommissionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCommissionCommand request, CancellationToken cancellationToken)
    {
        var commissionRepo = _unitOfWork.Repository<Commission>();
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var commission = await commissionRepo.GetByIdAsync(request.Id);

        if (commission == null)
            throw new InvalidOperationException($"Commission {request.Id} not found");

        // Check if commission has employees
        var allEmployees = await employeeRepo.GetAllAsync();
        var hasEmployees = allEmployees.Any(e => e.CommissionId == request.Id);

        if (hasEmployees)
            throw new InvalidOperationException($"Cannot delete commission {request.Id} because it has assigned employees. Please unassign the employees first.");

        await commissionRepo.DeleteAsync(commission);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
