using MediatR;
using Tradeflow.Application.Commands.Commissions;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Commissions;

public class CreateCommissionCommandHandler : IRequestHandler<CreateCommissionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCommissionCommand request, CancellationToken cancellationToken)
    {
        var commission = new Commission
        {
            TargetAmount = request.TargetAmount,
            Percentage = request.Percentage,
            Notes = request.Notes
        };

        await _unitOfWork.Repository<Commission>().AddAsync(commission);
        await _unitOfWork.SaveChangesAsync();

        return commission.Id;
    }
}
