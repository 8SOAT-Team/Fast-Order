using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
public interface IPagamentoGateway
{
    Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId);
    Task<Pagamento?> GetById(Guid id);
    Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento);
}
