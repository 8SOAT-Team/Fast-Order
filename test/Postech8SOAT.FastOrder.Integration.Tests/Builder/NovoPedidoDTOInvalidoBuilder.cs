using Bogus;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class NovoPedidoDTOInvalidoBuilder:Faker<NovoPedidoDTO>
{
    public NovoPedidoDTOInvalidoBuilder()
    {
        CustomInstantiator(f => new NovoPedidoDTO()
        {
            ClienteId = Guid.Empty,
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }
    public NovoPedidoDTO Build() => Generate();
}
