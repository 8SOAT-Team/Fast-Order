using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.Expressions;

namespace Postech8SOAT.FastOrder.Domain.ValueObjects;

public record Cpf
{
    public string Value { get; private init; } = null!;
    private string? _sanitazedValue = null;

    public Cpf(string value)
    {
        VerifyValueConstraints(value);
        Value = value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string? ToString() => Value;

    private static void VerifyValueConstraints(string cpf)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpf), () => InvalidArgumentException.WithErrorMessage("Cpf é obrigatório."));
        DomainExceptionValidation.When(Expression.HasCpfLength().IsMatch(cpf) is false, () => InvalidArgumentException.WithErrorMessage("Cpf deve conter 11 dígitos."));
    }

    public string GetSanitized()
    {
        _sanitazedValue ??= Expression.DigitsOnly().Replace(Value, "");
        return _sanitazedValue!;
    }

    public static bool TryCreate(string document, out Cpf cpf)
    {
        cpf = null!;

        if (string.IsNullOrWhiteSpace(document) || Expression.HasCpfLength().IsMatch(document) is false)
        {
            return false;
        }

        cpf = new Cpf(document);
        return true;
    }

    public static implicit operator string(Cpf cpf) => cpf.Value;
}
