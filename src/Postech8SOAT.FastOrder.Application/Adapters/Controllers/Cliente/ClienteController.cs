using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Application.Adapters.Gateways.Clientes;
using Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes;
using Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes.Models;
using Postech8SOAT.FastOrder.Application.UseCases;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.Application.Adapters.Presenters.UseCases;

namespace Postech8SOAT.FastOrder.Application.Adapters.Controllers.Cliente;

public interface IClienteController
{
    Task<Result<ClienteIdentificadoModel>> IdentificarClienteAsync(string document);
}

public class ClienteController(ILogger logger, IClienteGateway clienteGateway)
{
    private readonly ILogger _logger = logger;
    private readonly IClienteGateway _clienteGateway = clienteGateway;

    public async Task<Result<ClienteIdentificadoModel>> IdentificarCliente(string document)
    {
        var isCpfValid = Cpf.TryCreate(document, out var cpf);

        if(isCpfValid is false)
        {
            return Result<ClienteIdentificadoModel>.Failure(new AppProblemDetails("", "Cpf inválido", "Bad_Request", "Cpf informado não é válido", document));
        }

        var useCase = new IdentificarClienteUseCase(_logger, _clienteGateway);

        var useCaseResult = await useCase.ResolveAsync(cpf);

        if (useCase.IsFailure)
        {
            return Result<ClienteIdentificadoModel>.Failure(useCase.GetErrors().AdaptUseCaseErrors().ToList());
        }

        return useCaseResult.HasValue ? Result<ClienteIdentificadoModel>.Succeed(ClientePresenter.AdaptClienteIdentificado(useCaseResult.Value!))
            : Result<ClienteIdentificadoModel>.Empty();
    }
}
