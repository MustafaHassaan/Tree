using MediatR;
using Tradeflow.Application.Commands.Customers;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Customers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerRepo = _unitOfWork.Repository<Customer>();
        var orderRepo = _unitOfWork.Repository<Order>();
        var customer = await customerRepo.GetByIdAsync(request.Id);

        if (customer == null)
            throw new InvalidOperationException($"Customer {request.Id} not found");

        // Check if customer has orders
        var allOrders = await orderRepo.GetAllAsync();
        var hasOrders = allOrders.Any(o => o.CustomerId == request.Id);

        if (hasOrders)
            throw new InvalidOperationException($"Cannot delete customer {request.Id} because it has associated orders. Please delete the orders first.");

        await customerRepo.DeleteAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
