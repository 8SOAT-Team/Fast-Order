using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Tests.Entities
{
    public class PedidoTest
    {
        [Fact]
        public void DeveCriarNovoPedidoComSucesso()
        {
            //Arrange
            var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            var pedido = new Pedido(Guid.NewGuid(), listaItens);
            //Assert
            Assert.NotNull(pedido);
        }

        [Fact]
        public void DeveLancarExceptionQuandoPedidoNaoTiverItens()
        {
            //Arrange
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            //Act
            Action act = () => new Pedido(Guid.NewGuid(), listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }

        [Fact]
        public void DeveLancarExceptionQuandoIdPedidoInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            Action act = () => new Pedido(Guid.Empty, Guid.NewGuid(), listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }

        [Fact]
        public void DeveLancarExceptionQuandoPedidoTiverIdClienteInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            Action act = () => new Pedido(Guid.NewGuid(), Guid.Empty, listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }
    }
}
