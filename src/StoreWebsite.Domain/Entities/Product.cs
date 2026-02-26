using StoreWebsite.Domain.Common;

namespace StoreWebsite.Domain.Entities;

public sealed class Product : BaseEntity
{
    private Product()
    {
    }

    private Product(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        CreatedAtUtc = DateTime.UtcNow;
        LastModifiedAtUtc = DateTime.UtcNow;
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime LastModifiedAtUtc { get; private set; }

    public static Product Create(string name, string description, decimal price, int stockQuantity)
    {
        Validate(name, description, price, stockQuantity);
        return new Product(name.Trim(), description.Trim(), price, stockQuantity);
    }

    public void Update(string name, string description, decimal price, int stockQuantity)
    {
        Validate(name, description, price, stockQuantity);

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        StockQuantity = stockQuantity;
        LastModifiedAtUtc = DateTime.UtcNow;
    }

    private static void Validate(string name, string description, decimal price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name is required.", nameof(name));
        }

        if (name.Length > 150)
        {
            throw new ArgumentException("Product name must be at most 150 characters.", nameof(name));
        }

        if (description.Length > 500)
        {
            throw new ArgumentException("Description must be at most 500 characters.", nameof(description));
        }

        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(price));
        }

        if (stockQuantity < 0)
        {
            throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));
        }
    }
}
