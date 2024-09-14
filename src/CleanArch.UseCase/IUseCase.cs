using CleanArch.UseCase.Faults;

namespace CleanArch.UseCase;

public interface IUseCase<TCommand>
{
    public Task Resolve(TCommand command);

    public bool IsFaulted { get; }

    public IReadOnlyCollection<UseCaseError> GetErrors();

}

public interface IUseCase<TCommand, TOut> : IUseCase<TCommand>
{
    TOut? UseCaseResult { get; }
}