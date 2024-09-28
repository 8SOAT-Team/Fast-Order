using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArch.UseCase;

public abstract class UseCaseBase<TCommand, TOut> : IUseCase<TCommand, TOut> where TOut : class
{
    protected readonly ILogger _logger;
    private readonly List<UseCaseError> _useCaseError = [];

    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
    };

    protected UseCaseBase(ILogger logger)
    {
        _logger = logger;
    }

    public bool IsFailure => _useCaseError.Count != 0;

    protected void AddError(UseCaseError error) => _useCaseError.Add(error);
    protected void AddError(IEnumerable<UseCaseError> errors) => _useCaseError.AddRange(errors);

    public async Task<Any<TOut>> ResolveAsync(TCommand command)
    {
        _logger.LogInfo($"Iniciando Resolve {typeof(TCommand)}");
        _logger.LogDebug($"Comando recebido: {JsonSerializer.Serialize(command, jsonSerializerOptions)}");

        try
        {
            _logger.LogDebug("Iniciando execucao do comando");

            var result = await Execute(command);

            _logger.LogInfo($"Execucao concluida {typeof(TCommand)}");
            _logger.LogDebug($"Resultado {JsonSerializer.Serialize(result, jsonSerializerOptions)}");

            return result.ToAny();
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

        return Any<TOut>.Empty;
    }

    public IReadOnlyCollection<UseCaseError> GetErrors() => _useCaseError;

    protected abstract Task<TOut?> Execute(TCommand command);
}