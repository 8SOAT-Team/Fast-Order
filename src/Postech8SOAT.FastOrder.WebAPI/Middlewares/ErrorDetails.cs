using System.Text.Json;

namespace Postech8SOAT.FastOrder.WebAPI.Middlewares;

public class ErrorDetails
{
    public int StatusCode { get; init; }
    public string Message { get; init; } = null!;
    public string Trace { get; init; } = null!;

    public override string ToString() => JsonSerializer.Serialize(this);
}