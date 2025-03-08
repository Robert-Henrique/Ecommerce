using Ecommerce.Domain.Orders;

namespace Ecommerce.Domain.Repositories;

public interface IOrderMongoRepository
{
    Task SaveAsync(OrderReadModel order);
    Task<OrderReadModel> GetByIdAsync(int orderId);
}