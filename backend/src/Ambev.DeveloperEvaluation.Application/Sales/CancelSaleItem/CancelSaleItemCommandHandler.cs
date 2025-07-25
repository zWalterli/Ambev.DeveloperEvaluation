using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Handler for canceling a sale item
/// </summary>
/// <param name="_saleRepository"></param>
/// <param name="_logger"></param>
public class CancelSaleItemCommandHandler(
    ISaleRepository _saleRepository,
    IMediator _mediator,
    ILogger<CancelSaleItemCommandHandler> _logger) : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        sale.Itens.FirstOrDefault(i => i.Id == request.ItemId)?.Cancel();

        if (sale.Itens.All(i => i.IsCancelled))
        {
            sale.Cancel();
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        EventCommand command = new(sale.Id, SaleEvent.SaleItemCancelled);
        await _mediator.Send(command, cancellationToken);
        _logger.LogInformation("Sale item with ID {ItemId} from sale with ID {SaleId} has been canceled.", request.ItemId, request.SaleId);

        return new CancelSaleItemResult();
    }
}
