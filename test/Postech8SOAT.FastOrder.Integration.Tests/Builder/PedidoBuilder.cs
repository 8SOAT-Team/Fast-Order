using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class PedidoBuilder:Faker<Pedido>
{
    public PedidoBuilder()
    {
        CustomInstantiator(f => new Pedido(clienteId: f.Random.Guid(), itens: new List<ItemDoPedido>()
            {
                new ItemDoPedidoBuilder().Build(),
                new ItemDoPedidoBuilder().Build()
            }));
    }

    public PedidoBuilder(Guid clientId)
    {
        CustomInstantiator(f => new Pedido(clienteId: clientId, itens: new List<ItemDoPedido>()
            {
                new ItemDoPedidoBuilder().Build(),
                new ItemDoPedidoBuilder().Build()
            }));
    }
    public Pedido Build() => Generate();
}
