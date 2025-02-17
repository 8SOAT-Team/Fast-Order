
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Pagamentos;
using CleanArch.UseCase.Options;

public class ObterListaPedidosPendentesUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly ObterListaPedidosPendentesTest _useCase;

    public ObterListaPedidosPendentesUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new ObterListaPedidosPendentesTest(_loggerMock.Object, _pedidoGatewayMock.Object);
    }
    [Fact]
    public async Task Execute_PedidosPendentesExistem_RetornaListaDePedidosPendentes()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedidosPendentes = new List<Pedido>
            {
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){itemPedido}),
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){itemPedido})
            };

        _pedidoGatewayMock.Setup(pg => pg.GetAllPedidosPending()).ReturnsAsync(pedidosPendentes);

        // Act
        var result = await _useCase.Execute(null);

        // Assert
        Assert.NotNull(result); 
        Assert.Equal(2, result.Count);
        _pedidoGatewayMock.Verify(pg => pg.GetAllPedidosPending(), Times.Once); 
    }

    [Fact]
    public async Task Execute_NaoExistemPedidosPendentes_RetornaListaVazia()
    {
        // Arrange
        var pedidosPendentes = new List<Pedido>();
        _pedidoGatewayMock.Setup(pg => pg.GetAllPedidosPending()).ReturnsAsync(pedidosPendentes);

        // Act
        var result = await _useCase.Execute(null); 

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _pedidoGatewayMock.Verify(pg => pg.GetAllPedidosPending(), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        _pedidoGatewayMock.Setup(pg => pg.GetAllPedidosPending()).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(null));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message);  
    }
}

public class ObterListaPedidosPendentesTest : ObterListaPedidosPendentesUseCase
{
    public ObterListaPedidosPendentesTest(ILogger logger, IPedidoGateway pedidoGateway)
        : base(logger, pedidoGateway) { }

    public new Task<List<Pedido>?> Execute(Any<object> command)
    {
        return base.Execute(command);
    }

}
