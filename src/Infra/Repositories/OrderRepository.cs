using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Infra.ORM.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DefaultContext _context;

    public OrderRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }
}