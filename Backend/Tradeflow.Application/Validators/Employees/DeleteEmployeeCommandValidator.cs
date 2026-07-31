using FluentValidation;
using Tradeflow.Application.Commands.Employees;

namespace Tradeflow.Application.Validators.Employees;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID must be greater than 0");
    }
}
