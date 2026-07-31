using MediatR;
using Tradeflow.Application.Commands.Commissions;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Commissions;

public class UpdateCommissionCommandHandler : IRequestHandler<UpdateCommissionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCommissionCommand request, CancellationToken cancellationToken)
    {
        var commissionRepo = _unitOfWork.Repository<Commission>();
        var commission = await commissionRepo.GetByIdAsync(request.Id);

        if (commission == null)
            throw new InvalidOperationException($"Commission {request.Id} not found");

        commission.TargetAmount = request.TargetAmount;
        commission.Percentage = request.Percentage;
        commission.Notes = request.Notes;

        await commissionRepo.UpdateAsync(commission);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
