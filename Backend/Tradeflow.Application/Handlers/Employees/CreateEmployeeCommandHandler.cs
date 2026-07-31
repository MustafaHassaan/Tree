using MediatR;
using Tradeflow.Application.Commands.Employees;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Employees;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            Name = request.Name,
            Role = request.Role,
            Phone = request.Phone,
            PasswordHash = request.Password,
            CommissionId = request.CommissionId
        };

        await _unitOfWork.Repository<Employee>().AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return employee.Id;
    }
}
