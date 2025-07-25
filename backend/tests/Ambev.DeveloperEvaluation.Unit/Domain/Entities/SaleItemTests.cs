using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Tests for SaleItem entity to ensure it behaves as expected.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests the constructor of SaleItem to ensure it initializes properties correctly.
    /// </summary>
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var item = SaleItemData.GenerateFixedItem("Produto X", 5, 10m);

        // Assert
        Assert.Equal("Produto X", item.ProductName);
        Assert.Equal(5, item.Quantity);
        Assert.Equal(10m, item.UnitPrice);
        Assert.Equal(0m, item.DiscountPercentage);
        Assert.False(item.IsCancelled);
        Assert.NotEqual(Guid.Empty, item.Id);
    }

    /// <summary>
    /// Tests the Total property to ensure it calculates the total correctly without any discount.
    /// </summary>
    [Fact]
    public void Total_ShouldCalculateCorrectly()
    {
        // Arrange
        var item = SaleItemData.GenerateFixedItem("Produto Y", 3, 20m);

        // Assert
        Assert.Equal(60m, item.Total);
    }

    /// <summary>
    /// Tests the Total property to ensure it calculates the total correctly with a discount.
    /// </summary>
    [Fact]
    public void ApplyDiscount_ShouldApply_WhenValidQuantity()
    {
        // Arrange
        var item = SaleItemData.GenerateItemWithQuantity(10);

        // Act
        item.ApplyDiscount(0.10m);

        // Assert
        Assert.Equal(0.10m, item.DiscountPercentage);
        var expectedTotal = item.Quantity * item.UnitPrice * 0.90m;
        Assert.Equal(expectedTotal, item.Total);
    }

    /// <summary>
    /// Tests the ApplyDiscount method to ensure it throws an exception when the quantity is less than four.
    /// </summary>
    [Fact]
    public void ApplyDiscount_ShouldThrow_WhenQuantityLessThanFour()
    {
        // Arrange
        var item = SaleItemData.GenerateItemWithQuantity(2);

        // Act & Assert
        var ex = Assert.Throws<DomainException>(() => item.ApplyDiscount(0.10m));
        Assert.Equal("Discount not allowed for less than 4 units", ex.Message);
    }

    /// <summary>
    /// Tests the Update method to ensure it changes the properties of SaleItem correctly.
    /// </summary>
    [Fact]
    public void Update_ShouldChangeProperties()
    {
        // Arrange
        var item = SaleItemData.GenerateFixedItem("Original", 3, 15m);

        // Act
        item.Update("Novo Produto", 7, 25m);

        // Assert
        Assert.Equal("Novo Produto", item.ProductName);
        Assert.Equal(7, item.Quantity);
        Assert.Equal(25m, item.UnitPrice);
    }

    /// <summary>
    /// Tests the Cancel method to ensure it sets the IsCancelled property to true.
    /// </summary>
    [Fact]
    public void Cancel_ShouldSetIsCancelledTrue()
    {
        // Arrange
        var item = SaleItemData.GenerateValidItem();

        // Act
        item.Cancel();

        // Assert
        Assert.True(item.IsCancelled);
    }

    /// <summary>
    /// Tests the GenerateMockedItem method to ensure it applies a discount and cancels the item when requested.
    /// </summary>
    [Fact]
    public void GenerateMockedItem_ShouldApplyDiscountAndCancel_WhenRequested()
    {
        // Arrange
        var item = SaleItemData.GenerateMockedItem(0.20m, true);

        // Assert
        Assert.True(item.IsCancelled);
        Assert.Equal(0.20m, item.DiscountPercentage);
        var expectedTotal = item.Quantity * item.UnitPrice * 0.80m;
        Assert.Equal(expectedTotal, item.Total);
    }
}