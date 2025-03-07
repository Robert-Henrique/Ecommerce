using Ecommerce.Domain.Customers;

namespace Ecommerce.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}