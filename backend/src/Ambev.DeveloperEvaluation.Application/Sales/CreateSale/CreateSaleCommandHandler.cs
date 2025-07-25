using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for creating a sale
/// </summary>
/// <param name="_saleRepository"></param>
/// <param name="_logger"></param>
public class CreateSaleCommandHandler(
    ISaleRepository _saleRepository,
    IMediator _mediator,
    ILogger<CreateSaleCommandHandler> _logger) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Sale sale = new(request.SaleNumber, request.Customer, request.Branch);
        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
        }

        sale.ApplyDiscountRules();
        await _saleRepository.AddAsync(sale, cancellationToken);

        EventCommand command = new(sale.Id, SaleEvent.SaleCreated);
        await _mediator.Send(command, cancellationToken);
        _logger.LogInformation("Event: SaleCreated - {SaleId}", sale.Id);

        return new CreateSaleResult(sale.Id);
    }
}
