using SalesApi.Application.Interfaces;
using SalesApi.Application.Requests;
using SalesApi.Domain.Models;
using SalesApi.Domain.Repositories;

namespace SalesApi.Application;

public class SaleService(ISaleRepository saleRepository, IProductRepository productRepository, SaleDiscountService saleDiscountService) : ISaleService
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly SaleDiscountService _saleDiscountService = saleDiscountService;

    public async Task<Sale> CreateSale(CreateSaleRequest request)
    {
        Sale sale = new()
        {
            SaleNumber = request.SaleNumber,
            SaleDate = DateTime.UtcNow,
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            Cancelled = false,
        };

        var saleItems = await CreateItemsAsync(request.Items);

        sale.Items = [.. saleItems];

        _saleDiscountService.ApplyDiscounts(sale);

        return await _saleRepository.Create(sale);
    }

    private async Task<IEnumerable<SaleItem>> CreateItemsAsync(IEnumerable<CreateSaleItemRequest> items)
    {
        items = items
        .GroupBy(i => i.ProductId)
        .Select(g => new CreateSaleItemRequest
        {
            ProductId = g.Key,
            Quantity = g.Sum(i => i.Quantity)
        });

        var itemIds = items.Select(i => i.ProductId).ToArray();
        var products = await _productRepository.ListByIdsAsync(itemIds);

        return from p in products
               join i in items on p.Id equals i.ProductId
               select SaleItem.CreateForProduct(p, i.Quantity);
    }

    public async Task<ICollection<Sale>> GetSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public void CancelSale(Guid id)
    {
        _saleRepository.Cancel(id);
    }
}
