using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Validation;
using System.Text.Json;

namespace CleanArch.UseCase;

public abstract class UseCaseBase<TCommand> : IUseCase<TCommand>
{
    protected readonly ILogger _logger;
    private readonly List<UseCaseError> _useCaseError = [];
    protected readonly IValidator<TCommand> _validator;

    protected UseCaseBase(ILogger logger, IValidator<TCommand>? validator = null)
    {
        _logger = logger;
        _validator = validator!;
    }

    public bool IsFaulted { get => _useCaseError.Count != 0; }

    protected void AddError(UseCaseError error) => _useCaseError.Add(error);
    protected void AddError(IEnumerable<UseCaseError> errors) => _useCaseError.AddRange(errors);

    public async Task Resolve(TCommand command)
    {
        _logger.LogInfo($"Starting to resolve {typeof(TCommand)}");
        _logger.LogDebug($"Received command: {JsonSerializer.Serialize(command)}");

        try
        {
            if (_validator != null)
            {
                _logger.LogDebug("Initializing command validation");

                var (isValid, errors) = ValidateCommandInput(command, _validator);

                if (!isValid)
                {
                    AddError(errors!.Select(x => new UseCaseError(UseCaseErrorType.BadRequest, $"{x.PropertyName}: {x.ErrorMessage}")));
                    return;
                }

                _logger.LogDebug("Provided command is valid");
            }

            _logger.LogDebug("Initializing command execution");
            await Execute(command);
            _logger.LogInfo($"Resolved completed {typeof(TCommand)}");
        }
        catch (UseCaseException ucex)
        {
            AddError(new UseCaseError(ucex.Code, ucex.Message));
            _logger.LogError(ucex.Message, ucex.InnerException);
        }
        catch (Exception ex)
        {
            AddError(new UseCaseError(UseCaseErrorType.InternalError, ex.Message));
            _logger.LogError(ex.Message, ex.InnerException);
        }
    }

    public IReadOnlyCollection<UseCaseError> GetErrors() => _useCaseError;

    protected abstract Task Execute(TCommand command);

    /// <summary>
    /// Performs a validation
    /// Calls Fault to stop the flow
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    protected virtual (bool, IList<ValidationError>?) ValidateCommandInput(TCommand command, IValidator<TCommand> validator)
    {
        var validationResult = validator.Validate(command);

        return (validationResult.IsValid, validationResult.Errors);
    }

    protected string SerializeValdationErrors(IList<ValidationError> errors) => JsonSerializer.Serialize(errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage }));
}

public abstract class UseCaseBase<TCommand, TOut> : UseCaseBase<TCommand>, IUseCase<TCommand, TOut>
{
    protected UseCaseBase(ILogger logger, IValidator<TCommand>? validator = null) : base(logger, validator) { }

    public TOut? UseCaseResult { get; private set; }

    protected override abstract Task<TOut> Execute(TCommand command);

    public new async Task Resolve(TCommand command)
    {
        _logger.LogInfo($"Starting to resolve {typeof(TCommand)}");
        _logger.LogDebug($"Received command: {JsonSerializer.Serialize(command)}");

        try
        {
            if (_validator != null)
            {
                _logger.LogDebug("Initializing command validation");

                ValidateCommandInput(command, _validator);

                _logger.LogDebug("Provided command is valid");
            }

            _logger.LogDebug("Initializing command execution");
            UseCaseResult = await Execute(command);
            _logger.LogInfo($"Resolved completed {typeof(TCommand)}");
        }
        catch (UseCaseException ucex)
        {
            AddError(new UseCaseError(ucex.Code, ucex.Message));
            _logger.LogError(ucex.Message, ucex.InnerException);
        }
        catch (Exception ex)
        {
            AddError(new UseCaseError(UseCaseErrorType.InternalError, ex.Message));
            _logger.LogError(ex.Message, ex.InnerException);
        }
    }
}
