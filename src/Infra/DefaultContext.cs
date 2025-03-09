using System.Reflection;
using Ecommerce.Domain.Customers;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Products;
using Ecommerce.Infra.ORM.Settings;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.ORM;

public class DefaultContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(EnvironmentVariables.GetDatabaseConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}