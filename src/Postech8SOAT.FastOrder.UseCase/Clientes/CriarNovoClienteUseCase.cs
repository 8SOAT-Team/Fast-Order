using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Clientes.Dtos;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Clientes;

public class CriarNovoClienteUseCase(ILogger logger, IClienteGateway clienteGateway)
     : UseCase<CriarNovoClienteDto, Cliente>(logger)
{
    private readonly IClienteGateway _clienteGateway = clienteGateway;

    protected override async Task<Cliente?> Execute(CriarNovoClienteDto novoCliente)
    {
        var existingCliente = await _clienteGateway.GetClienteByCpfAsync(novoCliente.Cpf);

        if (existingCliente != null)
        {
            AddError(new CleanArch.UseCase.Faults.UseCaseError(CleanArch.UseCase.Faults.UseCaseErrorType.BadRequest, "Cpf já cadastrado!"));
            return null;
        }

        var cliente = new Cliente(Guid.NewGuid(), novoCliente.Cpf, novoCliente.Nome, novoCliente.Email);
        return await _clienteGateway.InsertCliente(cliente);
    }
}