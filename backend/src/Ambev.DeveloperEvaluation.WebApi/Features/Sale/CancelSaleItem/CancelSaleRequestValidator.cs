using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// Validator for CancelSaleItemRequest
/// Validates that the sale ID is provided
/// </summary>
public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    public CancelSaleItemRequestValidator()
    {
        RuleFor(sale => sale.SaleId)
            .NotEmpty().WithMessage("Sale ID is required.");

        RuleFor(sale => sale.ItemId)
            .NotEmpty().WithMessage("Item ID is required.");
    }
}