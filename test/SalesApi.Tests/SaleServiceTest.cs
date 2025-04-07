using FluentAssertions;
using Moq;
using SalesApi.Application;
using SalesApi.Application.Requests;
using SalesApi.Domain.Exceptions;
using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;
using SalesApi.Tests.TestData;

namespace SalesApi.Tests;

public class SaleServiceTest
{
    [Fact]
    public async Task When_Try_To_Create_Sale_With_Invalid_Parameters_Should_Be_Throw_Exception()
    {
        // Arrange
        var mockSaleRepository = new Mock<ISaleRepository>();
        var mockProductRepository = new Mock<IProductRepository>();
        var mockSaleDiscountService = new Mock<SaleDiscountService>();
        var mockSaleService = new SaleService(
            mockSaleRepository.Object,
            mockProductRepository.Object,
            mockSaleDiscountService.Object);

        // Act
        var createSaleRequestWithoutItems = new CreateSaleRequest
        {
            SaleNumber = 12345,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = []
        };
        var createSaleItemRequestWithGreaterQuantity = new CreateSaleItemRequest
        {
            ProductId = Guid.NewGuid(),
            Quantity = 21
        };
        var createSaleRequestWithItemGreaterQuantity = new CreateSaleRequest
        {
            SaleNumber = 12345,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = [createSaleItemRequestWithGreaterQuantity]
        };

        Func<Task> tryCreateSaleWithoutItem = () => mockSaleService.CreateSale(createSaleRequestWithoutItems);
        Func<Task> tryCreateSaleWithItemGreaterQuantity = () => mockSaleService.CreateSale(createSaleRequestWithItemGreaterQuantity);

        // Assert
        await tryCreateSaleWithoutItem.Should().ThrowAsync<FluentValidation.ValidationException>()
            .WithMessage("The Items field is required*");
        await tryCreateSaleWithItemGreaterQuantity.Should().ThrowAsync<DomainException>()
            .WithMessage("You cannot buy more than 20 pieces of same item");
    }

    [Fact]
    public async Task Given_Sale_With_Valid_Parameters_Should_Be_Created()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();
        var mockSaleRepository = new Mock<ISaleRepository>();
        var mockProductRepository = new Mock<IProductRepository>();

        var productList = new List<Product>
        {
            product
        };

        var createSaleRequest = new CreateSaleRequest
        {
            SaleNumber = 12345,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = [new CreateSaleItemRequest
                    {
                        ProductId = product.Id,
                        Quantity = 1,
                        UnitPrice = product.Price
                    }]
        };
        var sale = new Sale
        {
            SaleNumber = createSaleRequest.SaleNumber,
            SaleDate = DateTime.UtcNow,
            CustomerId = createSaleRequest.CustomerId,
            BranchId = createSaleRequest.BranchId,
            Cancelled = false,
            Items = [SaleItem.CreateForProduct(product, 1)]
        };

        var mockSaleDiscountService = new Mock<SaleDiscountService>();
        var mockSaleService = new SaleService(
            mockSaleRepository.Object,
            mockProductRepository.Object,
            mockSaleDiscountService.Object);

        mockProductRepository.Setup(repo => repo.ListByIdsAsync(It.IsAny<Guid[]>()))
                    .ReturnsAsync(productList);

        mockSaleRepository.Setup(repo => repo.Create(It.IsAny<Sale>()))
        .ReturnsAsync(sale);

        // Act
        var createSale = await mockSaleService.CreateSale(createSaleRequest);

        // Assert
        createSale.Should().NotBeNull();
        createSale.Should().BeOfType<Sale>();
        createSale.Should().BeEquivalentTo(sale);
        createSale.SaleNumber.Should().Be(createSaleRequest.SaleNumber);
        createSale.CustomerId.Should().Be(createSaleRequest.CustomerId);
        createSale.BranchId.Should().Be(createSaleRequest.BranchId);
        createSale.Items.Should().HaveCount(1);
        createSale.Items.Should().BeEquivalentTo(sale.Items);
        createSale.Items[0].ProductId.Should().Be(product.Id);
        createSale.Items[0].UnitPrice.Should().Be(product.Price);
        createSale.Items[0].Total.Should().Be(product.Price);
        createSale.Items[0].Quantity.Should().Be(1);
        createSale.Items[0].Discount.Should().Be(0);
    }
}
