using FluentAssertions;
using SalesApi.Domain.Models;
using SalesApi.Tests.TestData;

namespace SalesApi.Tests;

public class SaleItemTests
{

    [Fact]
    public void When_Try_Create_New_Sale_Item_With_Invalid_Parameters_Should_Be_Throw_Exception()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        Action tryCreateProductAsNull = () => SaleItem.CreateForProduct(null!, 3);
        Action tryCreateWithZeroQuantity = () => SaleItem.CreateForProduct(product, 0);
        Action tryCreateWithNegativeQuantity = () => SaleItem.CreateForProduct(product, -1);

        // Assert
        tryCreateProductAsNull.Should().Throw<ArgumentNullException>();
        tryCreateWithZeroQuantity.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Quantity must be positive value*");
        tryCreateWithNegativeQuantity.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Quantity must be positive value*");
    }

    [Fact]
    public void When_Create_New_Sale_Item_Totals_Should_Be_Calculated()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var saleItem = SaleItem.CreateForProduct(product, 3);

        // Assert
        saleItem.UnitPrice.Should().Be(product.Price);
        saleItem.Total.Should().Be(product.Price * 3);
    }

    [Fact]
    public void Given_Sale_Item_When_Apply_Discount_Total_Amount_Should_Be_Reduced_With_Discount()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValid();

        // Act
        var originalTotalAmount = saleItem.Total;
        saleItem.ApplyDiscount(10);

        // Assert
        saleItem.Total.Should().Be(originalTotalAmount - (originalTotalAmount * 0.1M));
    }

    [Fact]
    public void Given_Sale_Item_When_Calculate_Discount_Result_Should_Be_Percentage_Without_Applied_Into_The_Product()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValid();

        // Act
        var originalTotalAmount = saleItem.Total;
        var amountDiscounted = saleItem.CalculateDiscount(10);

        // Assert
        amountDiscounted.Should().Be(originalTotalAmount * 0.1M);
        saleItem.Total.Should().Be(originalTotalAmount);
    }
}
