using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// This class provides test data for the SaleItem entity.
/// </summary>
public static class SaleItemData
{
    private static readonly Faker faker = new();

    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .CustomInstantiator(f => new SaleItem(
            productName: f.Commerce.ProductName(),
            quantity: f.Random.Int(1, 20),
            unitPrice: f.Random.Decimal(10m, 500m)
        ));

    /// <summary>
    /// Generates a valid SaleItem object with random data.
    /// </summary>
    /// <returns></returns>
    public static SaleItem GenerateValidItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a list of SaleItem objects with random data.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<SaleItem> GenerateMultipleItems(int count = 3)
    {
        return SaleItemFaker.Generate(count);
    }

    /// <summary>
    /// Generates a SaleItem object with a specified quantity.
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static SaleItem GenerateItemWithQuantity(int quantity)
    {
        return new SaleItem(
            productName: faker.Commerce.ProductName(),
            quantity: quantity,
            unitPrice: faker.Random.Decimal(10m, 500m)
        );
    }


    /// <summary>
    /// Generates a SaleItem with fixed values for product name, quantity, and unit price.
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="quantity"></param>
    /// <param name="unitPrice"></param>
    /// <returns></returns>
    public static SaleItem GenerateFixedItem(
        string productName = "Produto Teste",
        int quantity = 5,
        decimal unitPrice = 20m)
    {
        return new SaleItem(productName, quantity, unitPrice);
    }

    /// <summary>
    /// Generates a SaleItem with optional discount and cancellation status.
    /// </summary>
    /// <param name="discountPercentage"></param>
    /// <param name="isCancelled"></param>
    /// <returns></returns>
    public static SaleItem GenerateMockedItem(decimal discountPercentage = 0.10m, bool isCancelled = false)
    {
        var productName = faker.Commerce.ProductName();
        var quantity = faker.Random.Int(4, 20); // garantir que o desconto seja permitido
        var unitPrice = faker.Random.Decimal(10m, 200m);

        var item = new SaleItem(productName, quantity, unitPrice);

        if (discountPercentage > 0)
            item.ApplyDiscount(discountPercentage);

        if (isCancelled)
            item.Cancel();

        return item;
    }
}