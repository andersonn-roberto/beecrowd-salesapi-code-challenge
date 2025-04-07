using FluentValidation;
using SalesApi.Application.Interfaces;
using SalesApi.Application.Requests;
using SalesApi.Application.Validators;
using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;

namespace SalesApi.Application;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public void CreateProduct(CreateProductRequest request)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = new Product
        {
            Description = request.Description,
            Category = request.Category,
            Image = request.Image,
            Price = request.Price
        };

        _productRepository.Create(product);
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }
}
