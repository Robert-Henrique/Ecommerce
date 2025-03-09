using Ecommerce.Domain.Customers;
using Ecommerce.Domain.Helpers;

namespace Ecommerce.Domain.Orders;

public class Order : Entity<Order>
{
    public Customer Customer { get; protected set; }
    public DateTime OrderDate { get; protected set; }
    public decimal TotalAmount { get; protected set; }
    public OrderStatus Status { get; protected set; }

    private List<OrderItem> _orderItems = new();
    public IEnumerable<OrderItem> OrderItems => _orderItems;

    private Order() { }

    public Order(Customer customer, IEnumerable<OrderItem> items)
    {
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
        _orderItems = items.ToList();
        TotalAmount = _orderItems.Sum(item => item.TotalPrice);
    }

    public void Update(Customer customer, OrderStatus status, List<OrderItem> items)
    {
        Customer = customer;
        Status = status;
        _orderItems = items;
        TotalAmount = _orderItems.Sum(item => item.TotalPrice);
    }
}