using MediatR;
using Tradeflow.Application.Commands.Customers;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Customers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerRepo = _unitOfWork.Repository<Customer>();
        var customer = await customerRepo.GetByIdAsync(request.Id);

        if (customer == null)
            throw new InvalidOperationException($"Customer {request.Id} not found");

        customer.Name = request.Name;
        customer.Type = request.Type;
        customer.Address = request.Address;
        customer.Phone = request.Phone;

        await customerRepo.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
