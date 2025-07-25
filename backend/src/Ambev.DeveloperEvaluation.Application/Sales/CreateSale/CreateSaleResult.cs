namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Result of the Create Sale operation
/// </summary>
public class CreateSaleResult
{
    public CreateSaleResult(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }
}