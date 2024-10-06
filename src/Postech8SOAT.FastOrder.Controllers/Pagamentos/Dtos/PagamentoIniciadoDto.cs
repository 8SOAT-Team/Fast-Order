using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;

namespace Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;

public record PagamentoIniciadoDto(Guid Id, MetodosDePagamento MetodoDePagamento, StatusDoPagamento status, decimal ValorTotal, string PagamentoExternoId);
