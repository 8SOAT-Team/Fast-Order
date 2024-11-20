namespace Postech8SOAT.FastOrder.Gateways.Cache;

public abstract class CacheGateway(ICacheContext cache)
{
    protected abstract Dictionary<string, (string cacheKey, bool InvalidateCacheOnChanges)> CacheKeys { get; }

    protected Task InvalidateCacheOnChange(string? sufix = null)
    {
        Task[] invalidateTasks = [];

        foreach (var key in CacheKeys.Keys)
        {
            var (CacheKey, InvalidateCacheOnChanges) = CacheKeys[key];

            if (InvalidateCacheOnChanges)
            {
                var itemKey = $"{CacheKey}{(sufix is null ? "" : $":{sufix}")}";
                invalidateTasks = [.. invalidateTasks, cache.InvalidateCacheAsync(itemKey)];
            }
        }

        if (invalidateTasks.Length > 0)
        {
            return Task.WhenAll(invalidateTasks);
        }

        return Task.CompletedTask;
    }
}
