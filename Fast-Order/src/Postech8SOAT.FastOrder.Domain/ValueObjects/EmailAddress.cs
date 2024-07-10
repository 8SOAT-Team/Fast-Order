using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.Expressions;
using System.Net;
using System.Text.RegularExpressions;

namespace Postech8SOAT.FastOrder.Domain.ValueObjects;

public partial record EmailAddress
{
    public string Address { get; private init; } = null!;

    public EmailAddress(string address)
    {
        DomainExceptionValidation.When<InvalidEmailArgumentException>(IsValidEmail(address) is false);
        Address = address;
    }

    public override int GetHashCode() => Address.GetHashCode();

    public override string? ToString() => Address;

    private static bool IsValidEmail(string email) => string.IsNullOrEmpty(email) is false && Expression.ValidEmail().IsMatch(email);
}
