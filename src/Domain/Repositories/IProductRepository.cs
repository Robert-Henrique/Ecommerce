using Ecommerce.Domain.Products;

namespace Ecommerce.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}