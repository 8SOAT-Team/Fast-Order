using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Options;

namespace CleanArch.UseCase;

public interface IUseCase
{
    public bool IsFailure { get; }
    public IReadOnlyCollection<UseCaseError> GetErrors();
}

public interface IUseCase<TCommand, TOut> : IUseCase where TOut : class
{
    public Task<Any<TOut>> ResolveAsync(TCommand command);
}