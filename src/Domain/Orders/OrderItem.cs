using Ecommerce.Domain.Helpers;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Orders;

public class OrderItem : Entity<OrderItem>
{
    public ExternalIdentity Product { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal UnitPrice { get; protected set; }
    public decimal TotalPrice { get; protected set; }

    private OrderItem() { }

    public OrderItem(ExternalIdentity product, int quantity, decimal unitPrice)
    {
        Validate(quantity, unitPrice);

        Product = product ?? throw new ArgumentNullException(nameof(product));
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = unitPrice * quantity;
    }

    private static void Validate(int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("The quantity must be greater than zero.", nameof(quantity));

        if (unitPrice <= 0)
            throw new ArgumentException("The price must be greater than zero.", nameof(unitPrice));
    }
}