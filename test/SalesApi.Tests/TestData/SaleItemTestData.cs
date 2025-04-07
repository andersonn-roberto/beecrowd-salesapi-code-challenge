using Bogus;
using SalesApi.Domain.Models;

namespace SalesApi.Tests.TestData;

public static class SaleItemTestData
{
    static readonly Product product = ProductTestData.GenerateValid();

    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Quantity, f => f.Random.Int(min: 2, max: 20))
        .RuleFor(c => c.ProductId, f => product.Id)
        .RuleFor(c => c.UnitPrice, f => product.Price)
        .RuleFor(c => c.Total, (f, ci) => ci.Quantity * ci.UnitPrice - ci.Discount);

    public static SaleItem GenerateValid()
    {
        return SaleItemFaker.Generate();
    }
}
