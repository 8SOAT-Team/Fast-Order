using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record NovoPagamentoDTO
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MetodosDePagamento MetodoDePagamento { get; init; }
}
