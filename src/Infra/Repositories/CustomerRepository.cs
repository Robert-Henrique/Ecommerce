using Ecommerce.Domain.Customers;
using Ecommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.ORM.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DefaultContext _context;

    public CustomerRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}