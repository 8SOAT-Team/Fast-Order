using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Domain.Tests.Entities
{
    public class PagamentoTest
    {
        /*[Fact]
        public void DeveCriarNovoPagamentoComSucesso()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);

            var pedido = new Pedido(Guid.NewGuid(), listaItens);

            //Act
            var pagamento = new Pagamento(Guid.NewGuid(), pedido.Id, pedido, Domain.Entities.Enums.MetodoDePagamento.Pix, 100m, "idExterno");
            //Assert
            Assert.NotNull(pagamento);
        }

        [Fact]
        public void DeveLancarExcepetionQuandoPagamentoIdInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);

            var pedido = new Pedido(Guid.NewGuid(), listaItens);

            //Act
            Action act = () => new Pagamento(Guid.Empty, pedido.Id, pedido, Domain.Entities.Enums.MetodoDePagamento.Pix, 100m, "idExterno");
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }

        [Fact]
        public void DeveLancarExcepetionQuandoValorInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);

            var pedido = new Pedido(Guid.NewGuid(), listaItens);

            //Act
            Action act = () => new Pagamento(Guid.NewGuid(), pedido.Id, pedido, Domain.Entities.Enums.MetodoDePagamento.Pix, -1m, "idExterno");
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }*/
    }
}
