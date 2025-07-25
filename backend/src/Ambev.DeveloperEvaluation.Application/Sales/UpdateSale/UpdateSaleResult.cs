namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Result of the Update Sale operation
/// Contains the ID of the updated sale
/// </summary>
public class UpdateSaleResult
{
    public UpdateSaleResult(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets or sets the ID of the updated sale
    /// This is the unique identifier for the sale that was updated
    /// </summary>
    public Guid Id { get; set; }
}