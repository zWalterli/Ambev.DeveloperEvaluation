namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Response model for GetSale
/// </summary>
public class GetSaleResponse
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sale number, which is a unique identifier for the sale transaction
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Total amount of the sale, including all items and discounts
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Name of the customer associated with the sale
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale is cancelled
    /// This property is used to determine if the sale has been voided or not.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// List of items included in the sale
    /// Each item contains details such as product name, quantity, unit price, discount, and total price.
    /// </summary>
    public List<GetSaleItemResponse> Itens { get; set; } = new();
}

/// <summary>
/// Response model for sale items in a sale
/// Represents the details of each item sold in a sale, including product name, quantity, unit price, discount, and total price.
/// </summary>
public class GetSaleItemResponse
{
    /// <summary>
    /// Unique identifier for the sale item
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Name of the product sold in the sale item
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// Quantity of the product sold in the sale item
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of the product sold in the sale item
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Discount percentage applied to the sale item
    /// </summary>
    public decimal DiscountPercentage { get; private set; }

    /// <summary>
    /// Total price of the sale item after applying the discount
    /// </summary>
    public decimal Total => Quantity * UnitPrice * (1 - DiscountPercentage);

    /// <summary>
    /// Indicates whether the sale item is cancelled
    /// </summary>
    public bool IsCancelled { get; private set; }
}