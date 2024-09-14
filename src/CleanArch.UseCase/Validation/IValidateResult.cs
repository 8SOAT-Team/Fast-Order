namespace CleanArch.UseCase.Validation;

public interface IValidateResult
{
    bool IsValid { get; }
    IList<ValidationError> Errors { get; }
}
