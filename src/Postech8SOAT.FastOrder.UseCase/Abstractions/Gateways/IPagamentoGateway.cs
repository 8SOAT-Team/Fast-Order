using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
public interface IPagamentoGateway
{
    Task<List<Pagamento>> FindPagamentoByPedidoIdAsync(Guid pedidoId);
    Task<Pagamento?> GetByIdAsync(Guid id);
    Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento);
}
