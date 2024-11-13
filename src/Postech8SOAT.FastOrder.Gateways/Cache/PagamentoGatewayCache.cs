using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class PagamentoGatewayCache(IPagamentoGateway nextExecution, ICacheContext cache) : IPagamentoGateway
{
    private static readonly Dictionary<string, string> _cacheKeys = new()
    {
        [nameof(FindPagamentoByPedidoIdAsync)] = $"{nameof(PagamentoGatewayCache)}_{nameof(FindPagamentoByPedidoIdAsync)}",
        [nameof(GetByIdAsync)] = $"{nameof(PagamentoGatewayCache)}_{nameof(GetByIdAsync)}",
    };

    public async Task<List<Pagamento>> FindPagamentoByPedidoIdAsync(Guid pedidoId)
    {
        var cacheKey = _cacheKeys[nameof(FindPagamentoByPedidoIdAsync)];

        var result = await cache.GetItemByKeyAsync<List<Pagamento>>($"{cacheKey}_{pedidoId}");

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.FindPagamentoByPedidoIdAsync(pedidoId);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Pagamento?> GetByIdAsync(Guid id)
    {
        var cacheKey = _cacheKeys[nameof(GetByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Pagamento>($"{cacheKey}_{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }


    public Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento) => nextExecution.UpdatePagamentoAsync(pagamento);
}
