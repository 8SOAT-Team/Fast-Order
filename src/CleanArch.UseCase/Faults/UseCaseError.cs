namespace CleanArch.UseCase.Faults;

public record UseCaseError(UseCaseErrorType Code, string Description);
