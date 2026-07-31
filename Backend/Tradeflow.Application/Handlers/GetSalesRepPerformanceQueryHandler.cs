using MediatR;
using Tradeflow.Application.Queries;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers;

public class GetSalesRepPerformanceQueryHandler : IRequestHandler<GetSalesRepPerformanceQuery, SalesRepPerformanceDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesRepPerformanceQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SalesRepPerformanceDetailDto> Handle(GetSalesRepPerformanceQuery request, CancellationToken cancellationToken)
    {
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var orderRepo = _unitOfWork.Repository<Order>();
        var commissionRepo = _unitOfWork.Repository<Commission>();

        var employee = await employeeRepo.GetByIdAsync(request.SalesRepId);
        if (employee == null)
            throw new InvalidOperationException($"Sales Representative {request.SalesRepId} not found");

        var orders = await orderRepo.GetAllAsync();
        var salesRepOrders = orders.Where(o => o.SalesRepId == request.SalesRepId && o.Status == Domain.Enums.OrderStatus.Completed).ToList();

        var totalSales = salesRepOrders.Sum(o => o.TotalAmount);
        
        // Get commission details if assigned
        decimal? targetAmount = null;
        decimal? commissionPercentage = null;
        decimal earnedCommission = 0;
        decimal achievementPercentage = 0;

        if (employee.CommissionId.HasValue)
        {
            var commission = await commissionRepo.GetByIdAsync(employee.CommissionId.Value);
            if (commission != null)
            {
                targetAmount = commission.TargetAmount;
                commissionPercentage = commission.Percentage;
                
                // Calculate achievement percentage
                achievementPercentage = commission.TargetAmount > 0 ? (totalSales / commission.TargetAmount) * 100 : 0;
                
                // Calculate earned commission based on percentage of sales
                earnedCommission = (totalSales * commission.Percentage) / 100;
            }
        }

        return new SalesRepPerformanceDetailDto
        {
            SalesRepId = employee.Id,
            SalesRepName = employee.Name,
            TargetAmount = targetAmount,
            CommissionPercentage = commissionPercentage,
            TotalSales = totalSales,
            AchievementPercentage = achievementPercentage,
            EarnedCommission = earnedCommission,
            TotalOrders = salesRepOrders.Count
        };
    }
}
