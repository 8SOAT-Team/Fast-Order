using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Clientes;

public class IdentificarClienteUseCase(ILogger logger,
    IClienteGateway clienteGateway)
    : UseCase<Cpf, Cliente>(logger)
{
    private readonly IClienteGateway _clienteGateway = clienteGateway;

    protected override Task<Cliente?> Execute(Cpf document)
    {
        return _clienteGateway.GetClienteByCpfAsync(document);
    }
}