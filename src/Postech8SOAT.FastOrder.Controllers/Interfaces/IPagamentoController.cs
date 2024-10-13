using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IPagamentoController
{
    Task<Result<List<PagamentoResponseDTO>>> GetPagamentoByPedidoAsync(Guid pedidoId);
    Task<Result<PagamentoResponseDTO>> ConfirmarPagamento(Guid pagamentoId, StatusDoPagamento status);
    Task<Result<PagamentoResponseDTO>> IniciarPagamento(Guid pedidoId, MetodosDePagamento metodoDePagamento);
    Task<Result<PagamentoResponseDTO>> ReceberWebhookPagamento(string pagamentoExternoId);
}
