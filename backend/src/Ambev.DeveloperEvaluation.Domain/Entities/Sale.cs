namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    /// <summary>
    /// Unique identifier for the sale
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Sale number, unique identifier for the sale transaction
    /// </summary>
    public string SaleNumber { get; private set; }

    /// <summary>
    /// Date and time when the sale was made
    /// </summary>
    public DateTime SaleDate { get; private set; }

    /// <summary>
    /// Customer associated with the sale
    /// </summary>
    public string Customer { get; private set; }

    /// <summary>
    /// Branch where the sale was made
    /// </summary>
    public string Branch { get; private set; }

    /// <summary>
    /// Indicates whether the sale has been cancelled
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// List of items included in the sale
    /// </summary>
    public List<SaleItem> Itens { get; } = new();

    /// <summary>
    /// Total amount of the sale, calculated as the sum of all item totals
    /// </summary>
    public decimal TotalAmount => Itens.Sum(i => i.Total);

    /// <summary>
    /// Constructor to initialize a new Sale instance
    /// </summary>
    /// <param name="saleNumber"></param>
    /// <param name="customer"></param>
    /// <param name="branch"></param>
    public Sale(string saleNumber, string customer, string branch)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        Customer = customer;
        Branch = branch;
        IsCancelled = false;
    }

    /// <summary>
    /// Adds an item to the sale
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="quantity"></param>
    /// <param name="unitPrice"></param>
    /// <exception cref="DomainException"></exception>
    public void AddItem(string productName, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new DomainException("Cannot sell more than 20 items of a product.");

        var item = new SaleItem(productName.ToLower().Trim(), quantity, unitPrice);
        Itens.Add(item);
    }

    /// <summary>
    /// Applies discount rules to the sale items based on quantity
    /// </summary>
    /// <exception cref="DomainException"></exception>
    public void ApplyDiscountRules()
    {
        if (IsCancelled)
            return;

        var grouped = Itens
            .Where(i => !i.IsCancelled)
            .GroupBy(i => i.ProductName)
            .ToList();

        foreach (var group in grouped)
        {
            var totalQuantity = group.Sum(i => i.Quantity);

            if (totalQuantity > 20)
                throw new DomainException($"Cannot sell more than 20 items of product '{group.Key}'.");

            var discount = 0m;
            if (totalQuantity >= 10) discount = 0.20m;
            else if (totalQuantity >= 4) discount = 0.10m;

            foreach (var item in group)
            {
                item.ApplyDiscount(discount);
            }
        }
    }

    /// <summary>
    /// Updates the sale data such as sale number, customer, and branch
    /// </summary>
    /// <param name="saleNumber"></param>
    /// <param name="customer"></param>
    /// <param name="branch"></param>
    public void UpdateData(string saleNumber, string customer, string branch)
    {
        SaleNumber = saleNumber;
        Customer = customer;
        Branch = branch;
    }

    /// <summary>
    /// Clears all items from the sale
    /// </summary>
    public void ClearItems()
    {
        Itens.Clear();
    }

    /// <summary>
    /// Replaces the current items in the sale with a new list of items
    /// </summary>
    /// <param name="updatedItems"></param>
    public void ReplaceItems(IEnumerable<SaleItem> updatedItems)
    {
        Itens.Clear();
        Itens.AddRange(updatedItems);
    }

    /// <summary>
    /// Cancels the sale, marking it as cancelled and cancelling all items
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
        Itens.ForEach(i => i.Cancel());
    }
}
