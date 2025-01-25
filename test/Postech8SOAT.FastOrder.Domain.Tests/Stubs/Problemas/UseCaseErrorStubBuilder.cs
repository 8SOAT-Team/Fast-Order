using CleanArch.UseCase.Faults;

public static class UseCaseErrorStubBuilder
{
    public static UseCaseError Create(
        UseCaseErrorType code = UseCaseErrorType.InternalError,
        string description = "Internal Error Description")
    {
        return new UseCaseError(code, description);
    }
}