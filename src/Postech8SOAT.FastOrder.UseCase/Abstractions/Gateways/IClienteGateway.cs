using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
public interface IClienteGateway
{
    Task<Cliente?> GetClienteByCpfAsync(Cpf cpf);
    Task<Cliente> InsertCliente(Cliente cliente);
}
