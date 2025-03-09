using System.Text.RegularExpressions;

namespace Ecommerce.Domain.ValueObjects;

public class Phone
{
    public string Value { get; }

    private Phone() { }

    public Phone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The phone number cannot be empty", nameof(value));

        if (!Regex.IsMatch(value, @"^\+?[1-9]\d{1,14}$"))
            throw new ArgumentException("The provided phone number is invalid", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is not Phone phone) return false;
        return Value == phone.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}