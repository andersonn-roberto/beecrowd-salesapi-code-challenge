namespace SalesApi.Domain.Models;

public class Sale
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public bool Cancelled { get; set; }
    public decimal TotalAmount { get; set; }
    public IList<SaleItem> Items { get; set; } = [];

    public void RefreshTotalAmount()
    {
        TotalAmount = Items
            .Sum(i => i.Total);
    }
}
