using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

public interface IPagamentoUseCase
{
    Task<Pagamento?> GetPagamentoAsync(Guid pagamentoId);
    Task<Pagamento?> GetPagamentoByPedidoAsync(Guid pedidoId);
    Task<Pagamento> CreatePagamentoAsync(Pedido pedido, MetodoDePagamento metodoDePagamento);
    Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento);
    Task<List<Pagamento>> ListPagamentos();
    Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId);
    Task ConfirmarPagamento(Guid pagamentoId, StatusPagamento status);
}
