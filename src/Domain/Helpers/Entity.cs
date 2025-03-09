namespace Ecommerce.Domain.Helpers;

public abstract class Entity<T> where T : Entity<T>
{
    public int Id { get; protected set; }
}