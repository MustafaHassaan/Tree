using FluentValidation;
using Tradeflow.Application.Commands.Products;

namespace Tradeflow.Application.Validators.Products;

public class SoftDeleteProductCommandValidator : AbstractValidator<SoftDeleteProductCommand>
{
    public SoftDeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0");
    }
}
