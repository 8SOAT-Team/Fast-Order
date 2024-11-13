using Postech8SOAT.FastOrder.Gateways.Extensions;
using Postech8SOAT.FastOrder.Types.Results;
using StackExchange.Redis;
using System.Text.Json;

namespace Postech8SOAT.FastOrder.Gateways.Cache;

public class CacheContext(IConnectionMultiplexer connectionMultiplexer, JsonSerializerOptions jsonOptions) : ICacheContext
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task<Result<T>> GetItemByKeyAsync<T>(string key)
    {
        var item = await _database.StringGetAsync(key);

        if (item == RedisValue.Null)
        {
            return Result<T>.Empty();
        }
        var deserialized = item.ToString().TryDeserialize<T>(jsonOptions);

        return deserialized;
    }

    public async Task<Result<T>> SetNotNullStringByKeyAsync<T>(string key, T value, int expireInSec = 3600)
    {
        if(value is null)
        {
            return Result<T>.Empty();
        }

        var result = await SetStringByKeyAsync(key, JsonSerializer.Serialize(value, jsonOptions), expireInSec);

        if (result.IsSucceed)
        {
            return Result<T>.Succeed(value);
        }

        return Result<T>.Failure(new AppProblemDetails("Não foi possível gravar em cache", "internal_server_error", "Verifique os logs", key));
    }


    public async Task<Result<string>> SetStringByKeyAsync(string key, string value, int expireInSec = 3600)
    {
        var response = await _database.StringSetAsync(key, value, TimeSpan.FromSeconds(expireInSec));

        if (response == RedisValue.Null)
        {
            return Result<string>.Empty();
        }

        return Result<string>.Succeed(value);
    }
}
