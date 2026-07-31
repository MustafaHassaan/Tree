using FluentValidation;
using Tradeflow.Application.Commands;

namespace Tradeflow.Application.Validators;

public class AssignEmployeeToWarehouseCommandValidator : AbstractValidator<AssignEmployeeToWarehouseCommand>
{
    public AssignEmployeeToWarehouseCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Employee ID must be greater than 0");
        
        RuleFor(x => x.WarehouseId)
            .GreaterThan(0).WithMessage("Warehouse ID must be greater than 0");
    }
}
