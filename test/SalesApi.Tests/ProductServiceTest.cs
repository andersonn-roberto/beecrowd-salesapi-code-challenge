using FluentAssertions;
using Moq;
using SalesApi.Application;
using SalesApi.Application.Requests;
using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;
using SalesApi.Tests.TestData;

namespace SalesApi.Tests;

public class ProductServiceTest
{
    [Fact]
    public async Task Given_Valid_Product_When_Create_Then_Product_Should_Be_Created()
    {
        // Arrange
        var createProductRequest = new CreateProductRequest
        {
            Description = "Test Product",
            Category = "Test Category",
            Image = "test.jpg",
            Price = 10.0m
        };
        var product = new Product
        {
            Description = createProductRequest.Description,
            Category = createProductRequest.Category,
            Image = createProductRequest.Image,
            Price = createProductRequest.Price
        };
        var listProduct = new List<Product>
        {
            product
        };
        var mockProductRepository = new Mock<IProductRepository>();
        var productService = new ProductService(mockProductRepository.Object);

        mockProductRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(listProduct);

        // Act
        var actualListProduct = await productService.GetProductsAsync();

        // Assert
        actualListProduct.Should().NotBeNull();
        actualListProduct.Should().HaveCount(1);
        actualListProduct.Should().BeEquivalentTo(listProduct);
        var createdProduct = actualListProduct.FirstOrDefault();
        createdProduct.Should().NotBeNull();
        createdProduct.Should().BeOfType<Product>();
        createdProduct.Should().Be(product);
        createdProduct.Should().BeEquivalentTo(product);
        createdProduct.Description.Should().Be(createProductRequest.Description);
        createdProduct.Category.Should().Be(createProductRequest.Category);
        createdProduct.Image.Should().Be(createProductRequest.Image);
        createdProduct.Price.Should().Be(createProductRequest.Price);
    }
}
