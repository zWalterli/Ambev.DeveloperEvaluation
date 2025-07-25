using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// This class provides test data for the Sale entity.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Faker instance for generating Sale objects with random data.
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(x => x.Customer, f => f.Random.String(20))
        .RuleFor(x => x.SaleNumber, f => f.Random.Int(0, 2000).ToString())
        .RuleFor(x => x.Id, f => f.Random.Guid());
    
    /// <summary>
    /// Generates a valid Sale object with random data.
    /// This method is used for testing purposes to create instances of Sale with valid properties.
    /// The Sale object will have a random customer name, sale number, and unique identifier.
    /// </summary>
    /// <returns></returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
}