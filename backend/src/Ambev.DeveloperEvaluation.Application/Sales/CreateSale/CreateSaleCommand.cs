
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a sale
/// Contains sale details such as sale number, customer, branch, and items
/// Implements IRequest to be handled by MediatR
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Customer name for the sale
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Branch where the sale is made
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// List of items in the sale
    /// </summary>
    public List<CreateSaleItemDto> Items { get; set; } = new();
}

/// <summary>
/// Data Transfer Object for sale items
/// Contains product name, quantity, and unit price
/// </summary>
public class CreateSaleItemDto
{
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
