using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

public interface IFornecedorPagamentoGateway
{
    Task<FornecedorCriarPagamentoResponseDto> IniciarPagamento(MetodoDePagamento metodoDePagamento, string emailPagador, decimal valorTotal, Guid pedidoId);
    Task<FornecedorGetPagamentoResponseDto> ObterPagamento(string IdExterno);
}
