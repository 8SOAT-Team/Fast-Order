using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class ClienteGatewayCache(IClienteGateway nextExecution, ICacheContext cache) : IClienteGateway
{
    public async Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        var itemKey = $"{nameof(ClienteGatewayCache)}_{nameof(GetClienteByCpfAsync)}_{cpf.GetSanitized()}";

        var result = await cache.GetItemByKeyAsync<Cliente>(itemKey);

        if (result.HasValue)
        {
            return result.Value;
        }

        var client = await nextExecution.GetClienteByCpfAsync(cpf);
        _ = await cache.SetNotNullStringByKeyAsync(itemKey, client);

        return client;
    }

    public Task<Cliente> InsertCliente(Cliente cliente)
    {
        return nextExecution.InsertCliente(cliente);
    }
}
