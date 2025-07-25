﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// Request model for cancelling a sale item
/// </summary>
public class CancelSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale item to be cancelled
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique identifier of the item to be cancelled
    /// </summary>
    public Guid ItemId { get; set; }
}