using FluentValidation;
using Tradeflow.Application.Commands.Employees;

namespace Tradeflow.Application.Validators.Employees;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Employee name is required")
            .MaximumLength(100).WithMessage("Employee name cannot exceed 100 characters");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid employee role");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");

        RuleFor(x => x.Password)
            .MinimumLength(4).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password must be at least 4 characters");

        RuleFor(x => x.CommissionId)
            .GreaterThan(0).When(x => x.CommissionId.HasValue).WithMessage("Commission ID must be greater than 0");
    }
}
