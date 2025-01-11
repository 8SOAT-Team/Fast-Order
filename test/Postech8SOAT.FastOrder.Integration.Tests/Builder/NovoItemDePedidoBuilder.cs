using Bogus;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class NovoItemDePedidoBuilder:Faker<NovoItemDePedido>
{
    public NovoItemDePedidoBuilder()
    {
        CustomInstantiator(f => new NovoItemDePedido()
        {
            ProdutoId = RetornaIdProdutoUtil.RetornaIdProduto(),
            Quantidade = f.Random.Int(1, 10)
        });
    }

   public NovoItemDePedido Build() => Generate();
}
