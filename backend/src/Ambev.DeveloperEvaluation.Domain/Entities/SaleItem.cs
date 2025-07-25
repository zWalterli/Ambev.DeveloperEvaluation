namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    /// <summary>
    /// Unique identifier for the sale item
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Name of the product associated with the sale item
    /// </summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>
    /// Quantity of the product sold in this sale item
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of the product in this sale item
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Discount percentage applied to the sale item
    /// </summary>
    public decimal DiscountPercentage { get; private set; }

    /// <summary>
    /// Total price for the sale item, calculated as Quantity * UnitPrice * (1 - DiscountPercentage)
    /// </summary>
    public decimal Total => Quantity * UnitPrice * (1 - DiscountPercentage);

    /// <summary>
    /// Indicates whether the sale item has been cancelled
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Default constructor for SaleItem
    /// </summary>
    public SaleItem()
    {
        Id = Guid.NewGuid();
        IsCancelled = false;
    }

    /// <summary>
    /// Constructor to initialize a new SaleItem instance
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="quantity"></param>
    /// <param name="unitPrice"></param>
    public SaleItem(string productName, int quantity, decimal unitPrice) : this()
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        DiscountPercentage = decimal.Zero;
    }

    /// <summary>
    /// Updates the sale item with new values
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="quantity"></param>
    /// <param name="unitPrice"></param>
    public void Update(string productName, int quantity, decimal unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Applies a discount to the sale item
    /// </summary>
    /// <param name="discount"></param>
    /// <exception cref="DomainException"></exception>
    public void ApplyDiscount(decimal discount)
    {
        if (discount > 0 && Quantity < 4)
            throw new DomainException("Discount not allowed for less than 4 units");

        DiscountPercentage = discount;
    }

    /// <summary>
    /// Cancels the sale item, marking it as cancelled
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }
}
