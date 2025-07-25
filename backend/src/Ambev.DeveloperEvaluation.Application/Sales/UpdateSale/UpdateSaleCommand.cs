using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command to update an existing sale
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Unique identifier of the sale to update
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sale number, typically a unique identifier for the sale
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Customer associated with the sale
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Branch where the sale was made
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// List of items in the sale to be updated
    /// </summary>
    public List<UpdateSaleItemDto> Items { get; set; } = new();
}

/// <summary>
/// Result of the update sale command
/// </summary>
public class UpdateSaleItemDto
{
    /// <summary>
    /// Unique identifier of the item, if it exists
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Name of the product in the sale item
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the product in the sale item
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product in the sale item
    /// </summary>
    public decimal UnitPrice { get; set; }
}
