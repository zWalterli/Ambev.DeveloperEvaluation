namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Result class for GetSales operation
/// This class encapsulates the total count of sales and a list of sale details.
/// </summary>
public class GetSalesResult
{
    public GetSalesResult(int totalCount, List<GetSaleResult> sales)
    {
        TotalCount = totalCount;
        Sales = sales;
    }

    /// <summary>
    /// Total number of sales retrieved from the database
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// List of sales retrieved from the database
    /// </summary>
    public List<GetSaleResult> Sales { get; set; } = new();
}

/// <summary>
/// Result class for GetSale operation
/// This class contains details of a single sale, including its ID, sale number, date,
/// customer, branch, cancellation status, and a list of sale items.
/// Each sale item includes its ID, product name, quantity, unit price, discount percentage,
/// total price, and cancellation status.
/// This class is used to return detailed information about a specific sale.
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public string SaleNumber { get; set; }

    /// <summary>
    /// Date and time when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Customer associated with the sale
    /// </summary>
    public string Customer { get; set; }

    /// <summary>
    /// Branch where the sale was made
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Indicates whether the sale has been cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// List of items in the sale
    /// </summary>
    public List<GetSaleItemResult> Itens { get; set; } = new();
}


public class GetSaleItemResult
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal Total => Quantity * UnitPrice * (1 - DiscountPercentage);
    public bool IsCancelled { get; private set; }
}