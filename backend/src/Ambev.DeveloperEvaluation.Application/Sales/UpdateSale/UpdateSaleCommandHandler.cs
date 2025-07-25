using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler(
    ISaleRepository _repository,
    IMediator _mediator,
    ILogger<UpdateSaleCommandHandler> _logger) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new Exception($"Sale {request.Id} not found.");

        sale.UpdateData(request.SaleNumber, request.Customer, request.Branch);

        var existingItems = sale.Itens.ToDictionary(i => i.Id, i => i);

        var updatedItems = new List<SaleItem>();
        foreach (var itemDto in request.Items)
        {
            if (itemDto.Id.HasValue && existingItems.TryGetValue(itemDto.Id.Value, out var existing))
            {
                // Atualiza o item existente
                existing.Update(itemDto.ProductName, itemDto.Quantity, itemDto.UnitPrice);
                updatedItems.Add(existing);
            }
            else
            {
                // Novo item
                var newItem = new SaleItem(itemDto.ProductName, itemDto.Quantity, itemDto.UnitPrice);
                updatedItems.Add(newItem);
            }
        }

        sale.ReplaceItems(updatedItems);
        sale.ApplyDiscountRules();

        await _repository.UpdateAsync(sale);

        EventCommand command = new(sale.Id, SaleEvent.SaleUpdated);
        await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Event: SaleModified - {SaleId}", sale.Id);
        return new UpdateSaleResult(sale.Id);
    }
}
