using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.Context;

public class FastOrderContextTest
{
    private DbContextOptions<FastOrderContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<FastOrderContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CanAddCliente()
    {
        var options = CreateInMemoryOptions();

        string expectedNome; 
        using (var context = new FastOrderContext(options))
        {
            var cliente = ClienteStubBuilder.Create();
            expectedNome = cliente.Nome;
            context.Clientes.Add(cliente);
            context.SaveChanges();
        }

        using (var context = new FastOrderContext(options))
        {
            Assert.Equal(1, context.Clientes.Count());
            Assert.Equal(expectedNome, context.Clientes.Single().Nome);
        }
    }

    [Fact]
    public void CanAddProduto()
    {
        var options = CreateInMemoryOptions();

        string expectedNome; 
        using (var context = new FastOrderContext(options))
        {
            var produto = ProdutoStubBuilder.Create();
            expectedNome = produto.Nome;
            context.Produtos.Add(produto);
            context.SaveChanges();
        }

        using (var context = new FastOrderContext(options))
        {
            Assert.Equal(1, context.Produtos.Count());
            Assert.Equal(expectedNome, context.Produtos.Single().Nome);
        }
    }

    [Fact]
    public void CanAddPedido()
    {
        var options = CreateInMemoryOptions();

        using (var context = new FastOrderContext(options))
        {
            var pedido = PedidoStubBuilder.Create();
            context.Pedidos.Add(pedido);
            context.SaveChanges();
        }

        using (var context = new FastOrderContext(options))
        {
            Assert.Equal(1, context.Pedidos.Count());
            Assert.NotNull(context.Pedidos.Single().DataPedido);
        }
    }
}