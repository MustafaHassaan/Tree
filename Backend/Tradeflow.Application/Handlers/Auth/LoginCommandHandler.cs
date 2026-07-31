using MediatR;
using Tradeflow.Application.Commands.Auth;
using Tradeflow.Domain.Entities;
using Tradeflow.Domain.Interfaces;
using System.Linq;

namespace Tradeflow.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var employeeRepo = _unitOfWork.Repository<Employee>();
        var employeeWarehouseRepo = _unitOfWork.Repository<EmployeeWarehouse>();
        
        var allEmployees = await employeeRepo.GetAllAsync();
        var employee = allEmployees.FirstOrDefault(e => e.Phone == request.Phone);

        if (employee == null)
            throw new UnauthorizedAccessException("Invalid phone or password");

        // Verify password (simple comparison - in production use BCrypt or similar)
        if (!VerifyPassword(request.Password, employee.PasswordHash))
            throw new UnauthorizedAccessException("Invalid phone or password");

        // Get warehouse assignment if exists
        var allEmployeeWarehouses = await employeeWarehouseRepo.GetAllAsync();
        var employeeWarehouse = allEmployeeWarehouses.FirstOrDefault(ew => ew.EmployeeId == employee.Id);
        int? warehouseId = employeeWarehouse?.WarehouseId;

        return new LoginResponseDto
        {
            EmployeeId = employee.Id,
            Name = employee.Name,
            Role = employee.Role.ToString(),
            WarehouseId = warehouseId
        };
    }

    private bool VerifyPassword(string password, string hash)
    {
        // Simple hash verification - in production use BCrypt
        return password == hash;
    }
}
