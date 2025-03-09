namespace Ecommerce.Domain.ValueObjects;

public class ExternalIdentity
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }

    public ExternalIdentity(int id, string name)
    {
        Id = id;
        Name = name;
    }
}