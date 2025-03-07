using Ecommerce.Domain.Customers;
using Ecommerce.Domain.Helpers;

namespace Ecommerce.Domain.Orders;

public class Order : Entity<Order>
{
    public Customer Customer { get; protected set; }
    public DateTime OrderDate { get; protected set; }
    public decimal TotalAmount { get; protected set; }
    public OrderStatus Status { get; protected set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order() { }

    public Order(Customer customer, IEnumerable<OrderItem> items)
    {
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
        _orderItems = items.ToList();
        TotalAmount = _orderItems.Sum(item => item.TotalPrice);
    }
}