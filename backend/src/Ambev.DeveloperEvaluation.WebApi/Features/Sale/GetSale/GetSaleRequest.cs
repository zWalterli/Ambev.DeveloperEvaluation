namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the sale.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer name associated with the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch associated with the sale.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Order by clause for sorting the results
    /// </summary>
    /// <remarks>
    /// Format: "saleNumber asc, customer desc"
    /// Use comma-separated fields with optional direction (asc|desc).
    /// Example: "customer desc, branch asc"
    /// </remarks>
    public string OrderBy { get; set; } = string.Empty;
}