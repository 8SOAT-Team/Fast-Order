namespace Postech8SOAT.FastOrder.Domain.Exceptions;

public class InvalidArgumentException : DomainExceptionValidation
{
    private const string _errorMessage = "Parâmetro informado é inválido. (Parâmetro {0})";

    protected InvalidArgumentException(string error) : base(error) { }

    public static InvalidArgumentException InvalidParameter(string parameter) => new(string.Format(_errorMessage, parameter));
    public static InvalidArgumentException WithErrorMessage(string error) => new(error);
}
