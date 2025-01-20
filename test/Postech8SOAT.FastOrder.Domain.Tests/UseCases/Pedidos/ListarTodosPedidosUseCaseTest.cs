using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pedidos;

public class ListarTodosPedidosUseCaseTest
{

    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly ListarTodosPedidosTest _useCase;

    public ListarTodosPedidosUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new ListarTodosPedidosTest(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_PedidosExistem_RetornaListaDePedidos()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedidos = new List<Pedido>
            {
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){itemPedido}),
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){itemPedido})
            };

        _pedidoGatewayMock.Setup(pg => pg.GetAllAsync()).ReturnsAsync(pedidos);

        // Act
        var result = await _useCase.Execute(null); 

        // Assert
        Assert.NotNull(result);  
        Assert.Equal(2, result.Count);
        _pedidoGatewayMock.Verify(pg => pg.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_NaoExistemPedidos_RetornaListaVazia()
    {
        // Arrange
        var pedidos = new List<Pedido>();
        _pedidoGatewayMock.Setup(pg => pg.GetAllAsync()).ReturnsAsync(pedidos);

        // Act
        var result = await _useCase.Execute(null);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _pedidoGatewayMock.Verify(pg => pg.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        _pedidoGatewayMock.Setup(pg => pg.GetAllAsync()).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(null));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message); 
    }
}

public class ListarTodosPedidosTest : ListarTodosPedidosUseCase
{
    public ListarTodosPedidosTest(ILogger logger, IPedidoGateway pedidoGateway)
        : base(logger, pedidoGateway) { }

    public new Task<List<Pedido>?> Execute(Any<object> command)
    {
        return base.Execute(command);
    }

}