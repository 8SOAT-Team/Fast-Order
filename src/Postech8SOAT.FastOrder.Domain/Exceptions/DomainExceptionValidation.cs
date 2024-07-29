namespace Postech8SOAT.FastOrder.Domain.Exceptions;
public class DomainExceptionValidation(string error) : Exception(error)
{
    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new DomainExceptionValidation(error);
        }
    }

    public static void When<TException>(bool hasError) where TException : DomainExceptionValidation
    {
        if (hasError)
        {
            throw Activator.CreateInstance<TException>();
        }
    }

    public static void When<TException>(bool hasError, Func<TException> throwFunc) where TException : DomainExceptionValidation
    {
        if (hasError)
        {
            throw throwFunc();
        }
    }
}
