using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class ProdutoGatewayCache(IProdutoGateway nextExecution, ICacheContext cache) : IProdutoGateway
{
    private static readonly Dictionary<string, string> _cacheKeys = new()
    {
        [nameof(GetProdutoByIdAsync)] = $"{nameof(ProdutoGatewayCache)}_{nameof(GetProdutoByIdAsync)}",
        [nameof(GetProdutoCompletoByIdAsync)] = $"{nameof(ProdutoGatewayCache)}_{nameof(GetProdutoCompletoByIdAsync)}",
        [nameof(GetProdutosByCategoriaAsync)] = $"{nameof(ProdutoGatewayCache)}_{nameof(GetProdutosByCategoriaAsync)}",
        [nameof(ListarProdutosByIdAsync)] = $"{nameof(ProdutoGatewayCache)}_{nameof(ListarProdutosByIdAsync)}",
        [nameof(ListarTodosProdutosAsync)] = $"{nameof(ProdutoGatewayCache)}_{nameof(ListarTodosProdutosAsync)}",
    };

    public Task<Produto> CreateProdutoAsync(Produto produto) => nextExecution.CreateProdutoAsync(produto);

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var cacheKey = _cacheKeys[nameof(GetProdutoByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Produto>($"{cacheKey}_{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetProdutoByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Produto?> GetProdutoCompletoByIdAsync(Guid id)
    {
        var cacheKey = _cacheKeys[nameof(GetProdutoCompletoByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Produto>($"{cacheKey}_{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        var item = await nextExecution.GetProdutoByIdAsync(id);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var cacheKey = _cacheKeys[nameof(GetProdutosByCategoriaAsync)];

        var result = await cache.GetItemByKeyAsync<ICollection<Produto>>($"{cacheKey}_{categoriaId}");

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.GetProdutosByCategoriaAsync(categoriaId);
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids) => nextExecution.ListarProdutosByIdAsync(ids);

    public async Task<ICollection<Produto>> ListarTodosProdutosAsync()
    {
        var cacheKey = _cacheKeys[nameof(ListarTodosProdutosAsync)];

        var result = await cache.GetItemByKeyAsync<ICollection<Produto>>(cacheKey);

        if (result.HasValue)
        {
            return result.Value!;
        }

        var item = await nextExecution.ListarTodosProdutosAsync();
        _ = await cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }
}
