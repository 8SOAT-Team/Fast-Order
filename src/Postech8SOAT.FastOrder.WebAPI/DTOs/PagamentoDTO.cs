using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record PagamentoDTO
{
    public PagamentoDTO(MetodoDePagamento metodoDePagamento)
    {
        MetodoDePagamento = metodoDePagamento;
    }

    private PagamentoDTO() { }

    public Guid Id { get; private init; }
    public Guid PedidoId { get; private init; }
    public string? PagamentoExternoId { get; private init; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPagamento Status { get; private init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MetodoDePagamento MetodoDePagamento { get; init; }

    public decimal ValorTotal { get; private init; }

    public static PagamentoDTO Create(Guid id, Guid pedidoId, string? pagamentoExternoId, 
        StatusPagamento status, MetodoDePagamento metodoDePagamento, decimal valorTotal)
    {
        return new PagamentoDTO()
        {
            Id = id,
            PedidoId = pedidoId,
            PagamentoExternoId = pagamentoExternoId,
            Status = status,
            MetodoDePagamento = metodoDePagamento,
            ValorTotal = valorTotal,
        };
    }
}
