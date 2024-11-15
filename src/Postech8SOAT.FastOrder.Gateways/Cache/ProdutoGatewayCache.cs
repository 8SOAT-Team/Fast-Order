using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class ProdutoGatewayCache(IProdutoGateway nextExecution, ICacheContext cache) : CacheGateway(cache), IProdutoGateway
{
    private readonly ICacheContext cache = cache;

    private static readonly Dictionary<string, (string CacheKey, bool InvalidateCacheOnChanges)> _cacheKeys = new()
    {
        [nameof(GetProdutoByIdAsync)] = ($"{nameof(ProdutoGatewayCache)}:{nameof(GetProdutoByIdAsync)}", false),
        [nameof(GetProdutoCompletoByIdAsync)] = ($"{nameof(ProdutoGatewayCache)}:{nameof(GetProdutoCompletoByIdAsync)}", false),
        [nameof(GetProdutosByCategoriaAsync)] = ($"{nameof(ProdutoGatewayCache)}:{nameof(GetProdutosByCategoriaAsync)}", true),
        [nameof(ListarProdutosByIdAsync)] = ($"{nameof(ProdutoGatewayCache)}:{nameof(ListarProdutosByIdAsync)}", false),
        [nameof(ListarTodosProdutosAsync)] = ($"{nameof(ProdutoGatewayCache)}:{nameof(ListarTodosProdutosAsync)}", true),
        [nameof(CreateProdutoAsync)] = (nameof(Produto), false),
    };

    protected override Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> CacheKeys => _cacheKeys;

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var createdProduct = await nextExecution.CreateProdutoAsync(produto);

        await InvalidateCacheOnChange();

        var (cacheKey, _) = _cacheKeys[nameof(CreateProdutoAsync)];
        var key = $"{cacheKey}:{produto.Id}";
        await cache.SetNotNullStringByKeyAsync(cacheKey, createdProduct);

        return createdProduct;
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var (cacheKey, _) = _cacheKeys[nameof(GetProdutoByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Produto>($"{cacheKey}:{id}");

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
        var (cacheKey, _) = _cacheKeys[nameof(GetProdutoCompletoByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Produto>($"{cacheKey}:{id}");

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
        var (cacheKey, _) = _cacheKeys[nameof(GetProdutosByCategoriaAsync)];

        var result = await cache.GetItemByKeyAsync<ICollection<Produto>>($"{cacheKey}:{categoriaId}");

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
        var (cacheKey, _) = _cacheKeys[nameof(ListarTodosProdutosAsync)];

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
