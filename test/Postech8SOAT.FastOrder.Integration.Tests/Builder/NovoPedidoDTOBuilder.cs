using Bogus;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class NovoPedidoDTOBuilder:Faker<NovoPedidoDTO>
{
    public NovoPedidoDTOBuilder()
    {
        CustomInstantiator(f => new NovoPedidoDTO()
        {
            ClienteId = f.Random.Guid(),
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }

    public NovoPedidoDTOBuilder(Guid clienteId)
    {
        CustomInstantiator(f => new NovoPedidoDTO()
        {
            ClienteId = clienteId,
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }

    public NovoPedidoDTO Build() => Generate();
}


