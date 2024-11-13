using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class CategoriaGatewayCache(ICategoriaGateway nextExecution, ICacheContext cache) : ICategoriaGateway
{
    private static readonly Dictionary<string, string> _cacheKeys = new()
    {
        [nameof(GetAllCategoriasAsync)] = $"{nameof(CategoriaGatewayCache)}_{nameof(GetAllCategoriasAsync)}",
        [nameof(GetCategoriaByIdAsync)] = $"{nameof(CategoriaGatewayCache)}_{nameof(GetCategoriaByIdAsync)}",
    };

    public async Task<ICollection<Categoria>> GetAllCategoriasAsync()
    {
        var cacheKey = _cacheKeys[nameof(GetAllCategoriasAsync)];

        var result = await cache.GetItemByKeyAsync<ICollection<Categoria>>(cacheKey);

        if (result.HasValue)
        {
            return result.Value!;
        }

        return await nextExecution.GetAllCategoriasAsync();
    }

    public async Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        var cacheKey = _cacheKeys[nameof(GetCategoriaByIdAsync)];

        var result = await cache.GetItemByKeyAsync<Categoria>($"{cacheKey}_{id}");

        if (result.HasValue)
        {
            return result.Value;
        }

        return await nextExecution.GetCategoriaByIdAsync(id);
    }
}