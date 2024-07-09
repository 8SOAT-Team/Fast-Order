using System.Text.RegularExpressions;

namespace Postech8SOAT.FastOrder.Domain.ValueObjects;

public partial record EmailAddress
{
    private const string _invalidValueMessage = "Email não está em um formato válido.";
    public string Address { get; private init; } = null!;

    public EmailAddress(string address)
    {
        if(IsValidEmail(address) is false)
        {
            throw new ArgumentException(_invalidValueMessage, nameof(address));
        }

        Address = address;
    }

    public override int GetHashCode() => Address.GetHashCode();

    public override string? ToString() => Address;

    private bool IsValidEmail(string email) => string.IsNullOrEmpty(email) is false && ValidEmail().IsMatch(email);


    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex ValidEmail();
}
