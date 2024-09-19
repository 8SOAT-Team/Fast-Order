using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;
public class PedidoRepository:Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(FastOrderContext context) : base(context)
    {
    }
}

