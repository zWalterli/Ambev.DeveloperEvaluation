using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving sales data
/// This command includes parameters for filtering sales by ID, sale number, customer, and branch,
/// as well as pagination parameters for page number and page size.
/// </summary>
public class GetSaleCommand : IRequest<GetSalesResult>
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Sale number to filter the sales
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
    /// Page number for pagination
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Page size for pagination
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Order by clause for sorting the results
    /// </summary>
    public string OrderBy { get; set; } = string.Empty;
}