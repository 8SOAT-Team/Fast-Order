using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Application.Adapters.Gateways.Clientes;
using Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes;
using Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes.Models;
using Postech8SOAT.FastOrder.Application.UseCases;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Application.Adapters.Controllers.Cliente;

public interface IClienteController
{
    Task<Any<ClienteIdentificadoModel>> IdentificarClienteAsync(string document);
}

public class ClienteController(ILogger logger, IClienteGateway clienteGateway)
{
    private readonly ILogger _logger = logger;
    private readonly IClienteGateway _clienteGateway = clienteGateway;

    public async Task<Any<ClienteIdentificadoModel>> IdentificarCliente(string document)
    {
        var cpf = new Cpf(document);

        var useCase = new IdentificarClienteUseCase(_logger, _clienteGateway);

        var result = await useCase.ResolveAsync(cpf);

        return result.HasValue ? Any<ClienteIdentificadoModel>.Some(ClientePresenter.AdaptClienteIdentificado(result.Value!))
            : Any<ClienteIdentificadoModel>.Empty;
    }
}
