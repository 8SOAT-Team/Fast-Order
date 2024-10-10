using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;

public interface IFornecedorPagamentoGateway
{
    Task<FornecedorCriarPagamentoResponseDto> IniciarPagamento(MetodoDePagamento metodoDePagamento, string emailPagador, decimal valorTotal, Guid pedidoId);
    Task<FornecedorGetPagamentoResponseDto> ObterPagamento(string IdExterno);
}
