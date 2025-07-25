using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .Length(1, 50).WithMessage("Sale number must be between 1 and 50 characters.");

        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("Customer is required.")
            .Length(1, 100).WithMessage("Customer name must be between 1 and 100 characters.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("Branch is required.")
            .Length(1, 100).WithMessage("Branch name must be between 1 and 100 characters.");

        RuleForEach(sale => sale.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        });
    }
}