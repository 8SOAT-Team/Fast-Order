using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    Task<Pagamento?> GetByPedidoId(Guid pedidoId);
    Task<List<Pagamento>> ListByPedidoId(Guid pedidoId);
}