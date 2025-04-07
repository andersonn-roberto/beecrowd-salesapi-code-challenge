using Bogus;
using Microsoft.VisualBasic;
using SalesApi.Domain.Models;

namespace SalesApi.Tests.TestData;

public static class SaleTestData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.SaleNumber, f => f.Random.Int(min: 1000, max: 9999))
        .RuleFor(c => c.SaleDate, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)))
        .RuleFor(c => c.CustomerId, f => f.Random.Guid())
        .RuleFor(c => c.BranchId, f => f.Random.Guid());

    public static Sale GenerateValid()
    {
        return SaleFaker.Generate();
    }
}
