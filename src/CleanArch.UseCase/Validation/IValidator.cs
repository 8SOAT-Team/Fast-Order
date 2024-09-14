namespace CleanArch.UseCase.Validation;

public interface IValidator<TCommand>
{
    IValidateResult Validate(TCommand cmd);
}
