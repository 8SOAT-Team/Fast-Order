using CleanArch.UseCase;
using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.UseCases.Common;

public abstract class UseCase<TCommand, TOut>(ILogger logger) : UseCaseBase<TCommand, TOut>(logger) where TOut : class
{
    protected override bool ThrowExceptionOnFailure => true;

    public override async Task<Any<TOut>> ResolveAsync(TCommand command)
    {
		try
		{
			var resolveResult = await base.ResolveAsync(command);
            return resolveResult;
        }
		catch(DomainExceptionValidation dev)
		{
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, dev.Message));
            _logger.LogError(dev.Message, dev.InnerException);
        }
        catch (Exception ex)
		{
            AddError(new UseCaseError(UseCaseErrorType.InternalError, ex.Message));
            _logger.LogError(ex.Message, ex.InnerException);
        }

        return Any<TOut>.Empty;
    }
}
