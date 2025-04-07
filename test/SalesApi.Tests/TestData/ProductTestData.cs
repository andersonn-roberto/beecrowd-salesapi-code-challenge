using Bogus;
using SalesApi.Domain.Models;

namespace SalesApi.Tests.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(u => u.Description, f => f.Lorem.Sentence(4))
            .RuleFor(u => u.Price, f => f.Random.Decimal(min: 1, max: 2000.00M))
            .RuleFor(u => u.Image, f => f.Internet.Url())
            .RuleFor(p => p.Category, f => f.Lorem.Sentence(4));

    public static Product GenerateValid()
    {
        return ProductFaker.Generate();
    }
}
