using System.Threading.Tasks;
using SalesApi.Application.Interfaces;
using SalesApi.Application.Requests;
using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;

namespace SalesApi.Application;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public void CreateProduct(CreateProductRequest request)
    {
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
