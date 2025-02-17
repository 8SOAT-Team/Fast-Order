using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Tests.Domain.Entities;
public class ItemDoPedidoTest
{
    [Fact]
    public void DeveCriarNovoItemDoPedidoComSucesso()
    {
        //Act
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
        //Assert
        Assert.NotNull(itemPedido);
    }

    [Fact]
    public void DeveLancarExceptionAoCriarNovoItemDoPedidoSemQuantidade()
    {
        //Act
        Action act = () => new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 0);
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveLancarExceptionAoCriarNovoItemDoPedidoComIdProdutoInvalido()
    {
        //Act
        Action act = () => new ItemDoPedido(Guid.NewGuid(), Guid.Empty, 2);
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveLancarExceptionAoCriarNovoItemDoPedidoComIdPedidoInvalido()
    {
        //Act
        Action act = () => new ItemDoPedido(Guid.Empty, Guid.NewGuid(), 2);
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }
}