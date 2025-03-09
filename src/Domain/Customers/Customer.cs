using Ecommerce.Domain.Helpers;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Customers;

public class Customer : Entity<Customer>
{
    public string Name { get; protected set; }
    public Email Email { get; protected set; }
    public Phone Phone { get; protected set; }

    private Customer() { }

    public Customer(string name, Email email, Phone phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The name cannot be empty.", nameof(name));

        Name = name;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
    }
}