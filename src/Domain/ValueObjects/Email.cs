using System.Text.RegularExpressions;

namespace Ecommerce.Domain.ValueObjects;

public class Email
{
    public string Value { get; }

    private Email() { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The e-mail cannot be empty", nameof(value));

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("The provided email is invalid", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is not Email email) return false;
        return Value == email.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}