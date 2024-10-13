using Newtonsoft.Json;

namespace Postech8SOAT.FastOrder.Upstream.Pagamentos.MercadoPago.Dtos;

public record PagamentoWebhookDTO
{
    public long Id { get; set; }
    public string Action { get; set; } = null!;

    [JsonProperty("api_version")]
    public string ApiVersion { get; set; } = null!;
    public PagamentoWebhookDataDTO Data { get; set; } = null!;

    [JsonProperty("date_created")]
    public DateTime DateCreated { get; set; }

    [JsonProperty("live_mode")]
    public bool LiveMode { get; set; }
    public string Type { get; set; } = null!;

    [JsonProperty("user_id")]
    public string UserId { get; set; } = null!;
}


public record PagamentoWebhookDataDTO
{
    public string? Id { get; set; } = null!;
}