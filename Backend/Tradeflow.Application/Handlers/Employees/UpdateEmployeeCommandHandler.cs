using MediatR;
using Tradeflow.Application.Commands.Employees;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;

namespace Tradeflow.Application.Handlers.Employees;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var employee = await employeeRepo.GetByIdAsync(request.Id);

        if (employee == null)
            throw new InvalidOperationException($"Employee {request.Id} not found");

        employee.Name = request.Name;
        employee.Role = request.Role;
        employee.Phone = request.Phone;

        // Only update password if provided
        if (!string.IsNullOrEmpty(request.Password))
        {
            employee.PasswordHash = request.Password;
        }

        employee.CommissionId = request.CommissionId;

        await employeeRepo.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
