using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Infrastructure.Repositories;

public class SaleRepository(DefaultContext context) : ISaleRepository
{
    private readonly DefaultContext _context = context;

    public async Task<Sale> Create(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return sale;
    }

    public async Task<ICollection<Sale>> GetAllAsync()
    {
        return await _context.Sales.Include(s => s.Items).ToListAsync();
    }

    public void Cancel(Guid id)
    {
        var sale = _context.Sales.Find(id);
        if (sale != null)
        {
            sale.Cancelled = true;
            _context.Sales.Update(sale);
            _context.SaveChanges();
        }
    }
}
