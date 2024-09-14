namespace CleanArch.UseCase.Validation;

public record ValidationError(string PropertyName, string ErrorMessage);
