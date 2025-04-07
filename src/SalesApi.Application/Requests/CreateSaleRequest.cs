namespace SalesApi.Application.Requests;

public class CreateSaleRequest
{
    public int SaleNumber { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public ICollection<CreateSaleItemRequest> Items { get; set; } = [];
    public bool SaleLimitReached => Items.GroupBy(i => i.ProductId)
        .Any(g => g.Sum(i => i.Quantity) > 20);
}

public class CreateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
