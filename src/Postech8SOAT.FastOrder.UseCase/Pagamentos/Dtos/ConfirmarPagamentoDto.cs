using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

public record ConfirmarPagamentoDto(Guid PagamentoId, StatusPagamento Status);