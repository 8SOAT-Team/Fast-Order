using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

public record IniciarPagamentoDto(Guid PedidoId, MetodoDePagamento MetodoDePagamento);
