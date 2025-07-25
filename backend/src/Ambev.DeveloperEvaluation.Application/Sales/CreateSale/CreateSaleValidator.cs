using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand
/// Ensures that the sale data is valid before processing
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .MaximumLength(20);

        RuleFor(x => x.Customer)
            .NotEmpty().WithMessage("Customer is required.")
            .MaximumLength(100);

        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("Branch is required.")
            .MaximumLength(100);

        RuleFor(x => x.Items)
            .NotNull().WithMessage("At least one item is required.")
            .Must(x => x.Count > 0).WithMessage("At least one item is required.");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemDtoValidator());

        RuleFor(x => x.Items)
            .Must(NotExceedQuantityPerProduct)
            .WithMessage("Cannot sell more than 20 items of the same product.");
    }

    private bool NotExceedQuantityPerProduct(List<CreateSaleItemDto> items)
    {
        return items
            .GroupBy(i => i.ProductName)
            .All(g => g.Sum(i => i.Quantity) <= 20);
    }
}

/// <summary>
/// Validator for CreateSaleItemDto
/// Ensures that the item data is valid before processing
/// </summary>
public class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
{
    public CreateSaleItemDtoValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}
