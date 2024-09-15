using CleanArch.UseCase;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Application.Adapters.Gateways.Clientes;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Application.UseCases;

public interface IIdentificarClienteUseCase : IUseCase<Cpf, Cliente> { }

public class IdentificarClienteUseCase(ILogger logger,
    IClienteGateway clienteGateway)
    : UseCaseBase<Cpf, Cliente>(logger)
{
    private readonly IClienteGateway _clienteGateway = clienteGateway;

    protected override Task<Cliente?> Execute(Cpf document)
    {
        return _clienteGateway.GetClienteByCpfAsync(document);
    }
}