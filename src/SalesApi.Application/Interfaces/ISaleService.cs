using SalesApi.Application.Requests;
using SalesApi.Domain.Models;

namespace SalesApi.Application.Interfaces;

public interface ISaleService
{
    Task<Sale> CreateSale(CreateSaleRequest request);
    Task<ICollection<Sale>> GetSalesAsync();
    void CancelSale(Guid id);
}
