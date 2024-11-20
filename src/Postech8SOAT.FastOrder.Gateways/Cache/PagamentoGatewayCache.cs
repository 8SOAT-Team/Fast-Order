using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class PagamentoGatewayCache(IPagamentoGateway nextExecution, ICacheContext cache) : CacheGateway(cache), IPagamentoGateway
{
    private readonly ICacheContext cache = cache;

    private static readonly Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> _cacheKeys = new()
    {
        [nameof(FindPagamentoByPedidoIdAsync)] = ($"{nameof(PagamentoGatewayCache)}:{nameof(FindPagamentoByPedidoIdAsync)}", false),
        [nameof(GetByIdAsync)] = ($"{nameof(PagamentoGatewayCache)}:{nameof(GetByIdAsync)}", false),
        [nameof(UpdatePagamentoAsync)] = ($"{nameof(PagamentoGatewayCache)}:{nameof(UpdatePagamentoAsync)}", false),
    };

    protected override Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> CacheKeys => _cacheKeys;

    public async Task<List<Pagamento>> FindPagamentoByPedidoIdAsync(Guid pedidoId)
    {
        var (cacheKey, _) = CacheKeys[nameof(FindPagamentoByPedidoIdAsync)];
        var key = $"{cacheKey}:{pedidoId}";

        var result = await cache.GetItemByKeyAsync<List<Pagamento>>(key);

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.FindPagamentoByPedidoIdAsync(pedidoId);
        _ = await cache.SetNotNullStringByKeyAsync(key, item);

        return item;
    }

    public async Task<Pagamento?> GetByIdAsync(Guid id)
    {
        var (cacheKey, _) = CacheKeys[nameof(GetByIdAsync)];
        var key = $"{cacheKey}:{id}";

        var result = await cache.GetItemByKeyAsync<Pagamento>(key);

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(key, item);

        return item;
    }


    public async Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento)
    {
        var pagamentoAtualizado = await nextExecution.UpdatePagamentoAsync(pagamento);

        var (cacheKey, _) = CacheKeys[nameof(UpdatePagamentoAsync)];
        var key = $"{cacheKey}:{pagamento.Id}";
        await cache.InvalidateCacheAsync(key);
        await cache.SetNotNullStringByKeyAsync(key, pagamentoAtualizado);

        return pagamentoAtualizado;
    }
}
