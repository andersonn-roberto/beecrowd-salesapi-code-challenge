namespace SalesApi.Application.Requests;

public class CreateProductRequest
{
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.0m;
}
