using FluentValidation;
using Tradeflow.Application.Commands;

namespace Tradeflow.Application.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be greater than 0");
        
        RuleFor(x => x.SalesRepId)
            .GreaterThan(0).WithMessage("Sales Representative ID must be greater than 0");
        
        RuleFor(x => x.WarehouseId)
            .GreaterThan(0).WithMessage("Warehouse ID must be greater than 0");
        
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item");
        
        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .GreaterThan(0).WithMessage("Product ID must be greater than 0");
                
                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0");
            });
    }
}
