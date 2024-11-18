using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class ClienteGatewayCache(IClienteGateway nextExecution, ICacheContext cache) : CacheGateway(cache), IClienteGateway
{
    private readonly ICacheContext cache = cache;

    private static readonly Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> _cacheKeys = new()
    {
        [nameof(GetClienteByCpfAsync)] = ($"{nameof(ClienteGatewayCache)}:{nameof(GetClienteByCpfAsync)}", false),
    };

    protected override Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> CacheKeys => _cacheKeys;
    

    public async Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        var itemKey = $"{_cacheKeys[nameof(GetClienteByCpfAsync)]}:{cpf.GetSanitized()}";

        var result = await cache.GetItemByKeyAsync<Cliente>(itemKey);

        if (result.HasValue)
        {
            return result.Value;
        }

        var client = await nextExecution.GetClienteByCpfAsync(cpf);
        _ = await cache.SetNotNullStringByKeyAsync(itemKey, client);

        return client;
    }

    public async Task<Cliente> InsertCliente(Cliente cliente)
    {
        var clienteInserido = await nextExecution.InsertCliente(cliente);

        var itemKey = $"{_cacheKeys[nameof(GetClienteByCpfAsync)]}:{clienteInserido.Cpf.GetSanitized()}";
        await cache.SetNotNullStringByKeyAsync(itemKey, clienteInserido);

        return clienteInserido;
    }
}
