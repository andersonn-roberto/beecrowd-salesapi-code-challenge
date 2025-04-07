using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Infrastructure.Repositories;

public class ProductRepository(DefaultContext context) : IProductRepository
{
    private readonly DefaultContext _context = context;

    public void Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public async Task<ICollection<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ICollection<Product>> ListByIdsAsync(Guid[] ids)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}
