using FluentValidation;
using Tradeflow.Application.Commands.Commissions;

namespace Tradeflow.Application.Validators.Commissions;

public class CreateCommissionCommandValidator : AbstractValidator<CreateCommissionCommand>
{
    public CreateCommissionCommandValidator()
    {
        RuleFor(x => x.TargetAmount)
            .GreaterThan(0).WithMessage("Target amount must be greater than 0");

        RuleFor(x => x.Percentage)
            .GreaterThan(0).WithMessage("Percentage must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Percentage cannot exceed 100");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters");
    }
}
