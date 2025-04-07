using SalesApi.Application.Requests;
using SalesApi.Domain.Models;

namespace SalesApi.Application.Interfaces;

public interface IProductService
{
    void CreateProduct(CreateProductRequest request);
    Task<ICollection<Product>> GetProductsAsync();
}
