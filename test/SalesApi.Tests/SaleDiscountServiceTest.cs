using FluentAssertions;
using SalesApi.Application;
using SalesApi.Domain.Models;
using SalesApi.Tests.TestData;

namespace SalesApi.Tests;

public class SaleDiscountServiceTest
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(3, 0)]
    [InlineData(4, 10)]
    [InlineData(9, 10)]
    [InlineData(10, 20)]
    [InlineData(20, 20)]
    [InlineData(21, 0)]
    public void Given_Sale_With_One_Item_When_Apply_Discount_Item_Total_Should_Be_Calculated(
        int quantity,
        decimal expectedDiscountPercentage)
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        var sale = SaleTestData.GenerateValid();
        var saleItem = SaleItem.CreateForProduct(product, quantity);
        sale.Items.Add(saleItem);
        var total = saleItem.UnitPrice * quantity;
        var discountService = new SaleDiscountService();

        // Act
        discountService.ApplyDiscounts(sale);

        // Assert
        saleItem.Discount.Should().Be(total * (expectedDiscountPercentage / 100));
        saleItem.Total.Should().Be(total - (total * (expectedDiscountPercentage / 100)));
        sale.TotalAmount.Should().Be(total - (total * (expectedDiscountPercentage / 100)));
    }
}
