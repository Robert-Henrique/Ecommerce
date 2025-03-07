using Ecommerce.Domain.Helpers;

namespace Ecommerce.Domain.Products;

public class Product : Entity<Product>
{
    public string Name { get; protected set; }
    public decimal Price { get; protected set; }

    public Product(string name, decimal price)
    {
        Validate(name, price);

        Name = name;
        Price = price;
    }

    private static void Validate(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The name cannot be empty", nameof(name));

        if (price <= 0)
            throw new ArgumentException("The price must be greater than zero", nameof(price));
    }
}