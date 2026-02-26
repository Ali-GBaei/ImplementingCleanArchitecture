namespace StoreWebsite.Web.Models.Products;

public sealed class ProductListItemViewModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public DateTime LastModifiedAtUtc { get; init; }
}
