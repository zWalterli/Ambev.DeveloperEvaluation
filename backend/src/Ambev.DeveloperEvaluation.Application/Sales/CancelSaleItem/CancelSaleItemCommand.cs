
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Command to cancel a sale item
/// Implements IRequest to process CancelSaleItemCommand
/// </summary>
public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
{
    /// <summary>
    /// Sale id
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Item id to be canceled from the sale
    /// </summary>
    public Guid ItemId { get; set; }
}