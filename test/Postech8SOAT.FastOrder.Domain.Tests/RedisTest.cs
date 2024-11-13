using StackExchange.Redis;

namespace Postech8SOAT.FastOrder.Domain.Tests;

public class RedisTest
{
    private static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect($"localhost:6379");

    [Fact]
    public async Task Ping()
    {
        var db = _redis.GetDatabase();

        await db.PingAsync();
    }
}
