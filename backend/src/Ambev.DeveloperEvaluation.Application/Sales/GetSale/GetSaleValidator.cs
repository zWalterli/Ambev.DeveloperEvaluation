using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for GetSaleCommand
/// Ensures that the sale data is valid before processing
/// </summary>
public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleCommandValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty().WithMessage("Page is required.");

        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage("Page size is required.");
    }
}
