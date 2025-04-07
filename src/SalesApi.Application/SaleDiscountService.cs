using SalesApi.Domain.Models;

namespace SalesApi.Application
{
    public class SaleDiscountService
    {
        private static readonly List<(int MinQuantity, int MaxQuantity, int Percentage)> Discounts =
        [
            (4, 9, 10),
            (10, 20, 20),
        ];

        public void ApplyDiscounts(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                var discount = Discounts.FirstOrDefault(d => item.Quantity >= d.MinQuantity && item.Quantity <= d.MaxQuantity);

                item.ApplyDiscount(discount.Percentage);
            }

            sale.RefreshTotalAmount();
        }
    }
}
