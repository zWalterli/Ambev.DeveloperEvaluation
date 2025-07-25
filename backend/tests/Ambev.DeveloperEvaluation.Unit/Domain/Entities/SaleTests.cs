using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Tests for Sale entity to ensure it behaves as expected.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests the constructor of Sale to ensure it initializes properties correctly.
    /// </summary>
    [Fact]
    public void AddItem_ShouldAdd_WhenValid()
    {
        var sale = new Sale("001", "Cliente A", "Filial A");
        sale.AddItem("Produto A", 5, 10m);

        Assert.Single(sale.Itens);
        Assert.Equal("produto a", sale.Itens[0].ProductName);
        Assert.Equal(50m, sale.Itens[0].Total);
    }

    /// <summary>
    /// Tests the AddItem method to ensure it throws an exception when trying to add more than 20 items.
    /// </summary>
    [Fact]
    public void AddItem_ShouldThrow_WhenMoreThan20()
    {
        var sale = new Sale("001", "Cliente A", "Filial A");
        Assert.Throws<DomainException>(() => sale.AddItem("Produto A", 21, 10m));
    }

    [Fact]
    public void ApplyDiscount_ShouldApply10Percent_When4Items()
    {
        var sale = new Sale("001", "Cliente A", "Filial A");
        sale.AddItem("Produto A", 4, 10m);
        sale.ApplyDiscountRules();

        Assert.Equal(36m, Math.Round(sale.Itens[0].Total, 2));
    }

    /// <summary>
    /// Tests the ApplyDiscount method to ensure it applies a 20% discount when there are 10 items.
    /// </summary>
    [Fact]
    public void ApplyDiscount_ShouldApply20Percent_When10Items()
    {
        var sale = new Sale("001", "Cliente A", "Filial A");
        sale.AddItem("Produto A", 10, 10m);
        sale.ApplyDiscountRules();

        Assert.Equal(80m, Math.Round(sale.Itens[0].Total, 2));
    }

    /// <summary>
    /// Tests the Cancel method to ensure it marks the sale and all items as cancelled.
    /// </summary>
    [Fact]
    public void Cancel_ShouldMarkSaleAndItemsAsCancelled()
    {
        var sale = new Sale("001", "Cliente A", "Filial A");
        sale.AddItem("Produto A", 5, 10m);
        sale.Cancel();

        Assert.True(sale.IsCancelled);
        Assert.All(sale.Itens, i => Assert.True(i.IsCancelled));
    }
}