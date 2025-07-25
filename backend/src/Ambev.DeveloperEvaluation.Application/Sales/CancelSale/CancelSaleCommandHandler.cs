using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for canceling a sale
/// Implements IRequestHandler to process CancelSaleCommand
/// </summary>
/// <param name="_saleRepository"></param>
/// <param name="_logger"></param>
public class CancelSaleCommandHandler(
    ISaleRepository _saleRepository,
    IMediator _mediator,
    ILogger<CancelSaleCommandHandler> _logger) : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        sale.Cancel();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        EventCommand command = new(sale.Id, SaleEvent.SaleCancelled);
        await _mediator.Send(command, cancellationToken);
        _logger.LogInformation("Event: SaleCanceled - {SaleId}", sale.Id);

        return new CancelSaleResult();
    }
}
