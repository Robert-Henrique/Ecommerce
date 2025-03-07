using Ecommerce.Domain.Orders;

namespace Ecommerce.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);
}