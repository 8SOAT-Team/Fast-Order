using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class PedidoGatewayCache(IPedidoGateway nextExecution, ICacheContext cache) : CacheGateway(cache: cache), IPedidoGateway
{

    private readonly ICacheContext cache = cache;

    private static readonly Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> _cacheKeys = new()
    {
        [nameof(GetAllAsync)] = ($"{nameof(PedidoGatewayCache)}:{nameof(GetAllAsync)}", true),
        [nameof(GetAllPedidosPending)] = ($"{nameof(PedidoGatewayCache)}:{nameof(GetAllPedidosPending)}", true),
        [nameof(GetByIdAsync)] = ($"{nameof(PedidoGatewayCache)}:{nameof(GetByIdAsync)}", true),
        [nameof(GetPedidoCompletoAsync)] = ($"{nameof(PedidoGatewayCache)}:{nameof(GetPedidoCompletoAsync)}", false),
        [nameof(CreateAsync)] = (nameof(Pedido), false),
        [nameof(UpdateAsync)] = (nameof(Pedido), false)
    };

    protected override Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> CacheKeys => _cacheKeys;


    public async Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido)
    {
        var pedidoAtualizado = await nextExecution.AtualizarPedidoPagamentoIniciadoAsync(pedido);

        await InvalidateCacheOnChange(pedido.Id.ToString());

        return pedidoAtualizado;
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        var pedidoCriado = await nextExecution.CreateAsync(pedido);

        var cacheKey = $"{_cacheKeys[nameof(CreateAsync)].cacheKey}:{pedido.Id}";
        await cache.SetNotNullStringByKeyAsync(cacheKey, pedidoCriado);

        return pedidoCriado;
    }

    public async Task<Pedido> UpdateAsync(Pedido pedido)
    {
        var pedidoAtualizado = await nextExecution.UpdateAsync(pedido);

        await InvalidateCacheOnChange(pedido.Id.ToString());

        var cacheKey = $"{_cacheKeys[nameof(UpdateAsync)].cacheKey}:{pedidoAtualizado.Id}";
        await cache.SetNotNullStringByKeyAsync(cacheKey, pedidoAtualizado);

        return pedidoAtualizado;
    }


    public async Task<List<Pedido>> GetAllAsync()
    {
        var (cacheKey, _) = _cacheKeys[nameof(GetAllAsync)];

        var result = await cache.GetItemByKeyAsync<List<Pedido>>(cacheKey);

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.GetAllAsync();
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<List<Pedido>> GetAllPedidosPending()
    {
        var (cacheKey, _) = _cacheKeys[nameof(GetAllPedidosPending)];

        var result = await cache.GetItemByKeyAsync<List<Pedido>>(cacheKey);

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.GetAllPedidosPending();
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Pedido?> GetByIdAsync(Guid id)
    {
        var (cacheKey, _) = _cacheKeys[nameof(GetAllPedidosPending)];

        var result = await cache.GetItemByKeyAsync<Pedido>($"{cacheKey}:{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Pedido?> GetPedidoCompletoAsync(Guid id)
    {
        var (cacheKey, _) = _cacheKeys[nameof(GetAllPedidosPending)];

        var result = await cache.GetItemByKeyAsync<Pedido>($"{cacheKey}:{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }
}
