using FluentValidation;
using Tradeflow.Application.Commands.Warehouses;

namespace Tradeflow.Application.Validators.Warehouses;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Warehouse ID must be greater than 0");
    }
}
