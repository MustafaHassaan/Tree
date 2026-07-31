using FluentValidation;
using Tradeflow.Application.Commands.Commissions;

namespace Tradeflow.Application.Validators.Commissions;

public class DeleteCommissionCommandValidator : AbstractValidator<DeleteCommissionCommand>
{
    public DeleteCommissionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Commission ID must be greater than 0");
    }
}
