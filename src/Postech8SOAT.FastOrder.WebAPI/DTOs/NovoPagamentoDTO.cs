using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record NovoPagamentoDTO
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MetodoDePagamento MetodoDePagamento { get; init; }
}
