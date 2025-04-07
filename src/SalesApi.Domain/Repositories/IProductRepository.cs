using SalesApi.Domain.Models;

namespace SalesApi.Domain.Repositories;

public interface IProductRepository
{
    void Create(Product product);
    Task<ICollection<Product>> GetAllAsync();
    Task<ICollection<Product>> ListByIdsAsync(Guid[] ids);
}
