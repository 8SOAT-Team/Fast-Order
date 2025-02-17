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

public class EncontrarPedidoPorIdUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly EncontrarPedidoPorIdTest _useCase;

    public EncontrarPedidoPorIdUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new EncontrarPedidoPorIdTest(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_PedidoEncontrado_RetornaPedido()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(pedidoId, Guid.NewGuid(), listaItens);
        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedidoId)).ReturnsAsync(pedido);

        // Act
        var result = await _useCase.Execute(pedidoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pedidoId, result.Id); 
        _pedidoGatewayMock.Verify(pg => pg.GetByIdAsync(pedidoId), Times.Once); 
    }

    [Fact]
    public async Task Execute_PedidoNaoEncontrado_RetornaNull()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido)null);

        // Act
        var result = await _useCase.Execute(pedidoId);

        // Assert
        Assert.Null(result);  
        _pedidoGatewayMock.Verify(pg => pg.GetByIdAsync(pedidoId), Times.Once);  
    }

    [Fact]
    public async Task Execute_PedidoNaoEncontrado_ExecutaLogger()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido)null);

        // Act
        var result = await _useCase.Execute(pedidoId);

        // Assert
        Assert.Null(result); 
    }

}


public class EncontrarPedidoPorIdTest : EncontrarPedidoPorIdUseCase
{
    public EncontrarPedidoPorIdTest(ILogger logger, IPedidoGateway pedidoGateway)
        : base(logger, pedidoGateway) { }

    public new Task<Pedido?> Execute(Guid pedidoId)
    {
        return base.Execute(pedidoId);
    }

}