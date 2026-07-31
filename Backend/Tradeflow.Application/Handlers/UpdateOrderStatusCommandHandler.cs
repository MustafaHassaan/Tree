using MediatR;
using Tradeflow.Application.Commands;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using Tradeflow.Domain.Enums;

namespace Tradeflow.Application.Handlers;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var orderRepo = _unitOfWork.Repository<Order>();
        var order = await orderRepo.GetByIdAsync(request.OrderId);

        if (order == null)
            throw new InvalidOperationException($"Order {request.OrderId} not found");

        // Validate status transition
        if (!IsValidStatusTransition(order.Status, request.Status))
        {
            throw new InvalidOperationException($"Invalid status transition from {order.Status} to {request.Status}");
        }

        order.Status = request.Status;
        await orderRepo.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private bool IsValidStatusTransition(OrderStatus current, OrderStatus newStatus)
    {
        return (current, newStatus) switch
        {
            (OrderStatus.Pending, OrderStatus.Approved) => true,
            (OrderStatus.Approved, OrderStatus.Completed) => true,
            (OrderStatus.Pending, OrderStatus.Cancelled) => true,
            (OrderStatus.Approved, OrderStatus.Cancelled) => true,
            _ => false
        };
    }
}
