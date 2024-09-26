namespace CleanArch.UseCase.Faults;

public class UseCaseException : Exception
{
    public UseCaseException(UseCaseErrorType code, string message) : base(message)
    {
        Code = code;
    }

    public UseCaseErrorType Code { get; set; }
}
