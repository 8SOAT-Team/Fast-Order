using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Gateways.Pagamentos.Dtos;

public record FornecedorGetPagamentoResponseDto(string IdExterno, StatusPagamento StatusPagamento);