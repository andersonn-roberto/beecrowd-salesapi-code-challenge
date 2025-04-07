using SalesApi.Domain.Models;

namespace SalesApi.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> Create(Sale sale);
    Task<ICollection<Sale>> GetAllAsync();
    void Cancel(Guid id);
}
