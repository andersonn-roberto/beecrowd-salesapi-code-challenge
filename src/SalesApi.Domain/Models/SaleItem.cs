namespace SalesApi.Domain.Models;

public class SaleItem
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public bool IsCancelled { get; set; }

    public static SaleItem CreateForProduct(Product p, int quantity)
    {
        SaleItem item = new()
        {
            ProductId = p.Id,
            Quantity = quantity,
            UnitPrice = p.Price,
            Total = p.Price * quantity,
            Discount = 0
        };

        item.RefreshTotalAmount();

        return item;
    }

    public void ApplyDiscount(decimal percentage)
    {
        Discount = CalculateDiscount(percentage);

        RefreshTotalAmount();
    }

    public decimal CalculateDiscount(decimal percentage)
    {
        if (percentage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage must be positive or zero value");
        }

        return Quantity * UnitPrice * (percentage / 100);
    }

    private void RefreshTotalAmount()
    {
        Total = Quantity * UnitPrice - Discount;
    }
}
