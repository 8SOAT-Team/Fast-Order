using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class PedidoGatewayCache(IPedidoGateway nextExecution, ICacheContext cache) : IPedidoGateway
{
    private static readonly Dictionary<string, string> _cacheKeys = new()
    {
        [nameof(GetAllAsync)] = $"{nameof(PedidoGatewayCache)}_{nameof(GetAllAsync)}",
        [nameof(GetAllPedidosPending)] = $"{nameof(PedidoGatewayCache)}_{nameof(GetAllPedidosPending)}",
        [nameof(GetByIdAsync)] = $"{nameof(PedidoGatewayCache)}_{nameof(GetByIdAsync)}_{{0}}",
        [nameof(GetPedidoCompletoAsync)] = $"{nameof(PedidoGatewayCache)}_{nameof(GetPedidoCompletoAsync)}_{{0}}",
    };

    public Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido) => nextExecution.AtualizarPedidoPagamentoIniciadoAsync(pedido);

    public Task<Pedido> CreateAsync(Pedido pedido) => nextExecution.CreateAsync(pedido);

    public Task<Pedido> UpdateAsync(Pedido pedido) => nextExecution.UpdateAsync(pedido);


    public async Task<List<Pedido>> GetAllAsync()
    {
        var cacheKey = _cacheKeys[nameof(GetAllAsync)];

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
        var cacheKey = _cacheKeys[nameof(GetAllPedidosPending)];

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
        var cacheKey = _cacheKeys[nameof(GetAllPedidosPending)];

        var result = await cache.GetItemByKeyAsync<Pedido>($"{cacheKey}_{id}");

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
        var cacheKey = _cacheKeys[nameof(GetAllPedidosPending)];

        var result = await cache.GetItemByKeyAsync<Pedido>($"{cacheKey}_{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }
}
