using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    Task<Pagamento?> GetByPedidoId(Guid pedidoId);
    Task<List<Pagamento>> ListByPedidoId(Guid pedidoId);
}