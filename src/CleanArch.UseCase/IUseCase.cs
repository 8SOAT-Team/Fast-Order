using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Options;

namespace CleanArch.UseCase;

public interface IUseCase<TCommand, TOut> where TOut : class
{
    public Task<Any<TOut>> ResolveAsync(TCommand command);

    public bool IsFailure { get; }

    public IReadOnlyCollection<UseCaseError> GetErrors();
}