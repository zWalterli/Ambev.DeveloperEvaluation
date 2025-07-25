using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    public GetSaleRequestValidator()
    {
        RuleFor(x => x.Page).NotEmpty().WithMessage("Page is required.");
        RuleFor(x => x.PageSize).NotEmpty().WithMessage("Page size is required.");
    }
}