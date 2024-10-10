using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface IPagamentoGateway
{
    Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId);
    Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento);
}
