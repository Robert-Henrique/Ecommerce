using MediatR;

namespace Ecommerce.Application.Orders;

public class CreateOrderCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public List<OrderItemCommand> OrderItems { get; set; }
}