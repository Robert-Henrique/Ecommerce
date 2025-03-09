using Ecommerce.Domain.Orders;

namespace Ecommerce.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Order order, CancellationToken cancellationToken = default);
}