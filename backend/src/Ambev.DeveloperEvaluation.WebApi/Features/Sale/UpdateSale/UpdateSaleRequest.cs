namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sale number, typically a unique identifier for the sale transaction
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the sale was made
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Name of the customer associated with the sale
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// List of items included in the sale
    /// Each item contains details such as product name, quantity, and unit price
    /// </summary>
    public List<UpdateSaleItemRequest> Items { get; set; } = new();
}

public class UpdateSaleItemRequest
{
    /// <summary>
    /// Unique identifier for the sale item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the product sold in the sale item
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the product sold in the sale item
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product sold in the sale item
    /// </summary>
    public decimal UnitPrice { get; set; }
}
