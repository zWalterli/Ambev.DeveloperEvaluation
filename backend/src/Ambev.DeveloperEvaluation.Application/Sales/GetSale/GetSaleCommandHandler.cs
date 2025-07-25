using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ViewModels;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command handler for retrieving sales data
/// This handler processes the GetSaleCommand and returns a paginated list of sales.
/// </summary>
/// <param name="_repository"></param>
/// <param name="_mapper"></param>
public class GetSaleCommandHandler(
    ISaleRepository _repository,
    IMapper _mapper) : IRequestHandler<GetSaleCommand, GetSalesResult>
{
    /// <summary>
    /// Handles the GetSaleCommand to retrieve sales data
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetSalesResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var getSalePaginated = new GetSalePaginated(
            request.Page, request.PageSize, request.Id, request.SaleNumber, request.Customer, request.Branch);

        var (totalCount, sales) = await _repository.GetPaginatedAsync(getSalePaginated, cancellationToken);

        var salesResult = _mapper.Map<List<GetSaleResult>>(sales);

        return new GetSalesResult(totalCount, salesResult);
    }
}
