namespace SalesApi.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}
