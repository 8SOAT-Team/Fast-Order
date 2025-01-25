using Moq;
using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;


namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pedidos;

public class AtualizarStatusDePreparoPedidoUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly AtualizarStatusDePreparoPedidoTest _useCase;

    public AtualizarStatusDePreparoPedidoUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new AtualizarStatusDePreparoPedidoTest(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_PedidoNaoEncontrado_ReturnsNullAndAddsError()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var novoStatus = StatusPedido.EmPreparacao;
        var dto = new NovoStatusDePedidoDTO { PedidoId = pedidoId, NovoStatus = novoStatus };

        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido?)null);

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Pedido não encontrado", useCaseErrors.FirstOrDefault()?.Description);
    }

    [Fact]
    public async Task Execute_StatusInvalido_ReturnsNullAndAddsError()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var novoStatus = (StatusPedido)999;
        var dto = new NovoStatusDePedidoDTO { PedidoId = pedidoId, NovoStatus = novoStatus };

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(pedidoId, Guid.NewGuid(), listaItens);

        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedidoId)).ReturnsAsync(pedido);

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Status de pedido inválido", useCaseErrors.FirstOrDefault().Description);
    }

    [Theory]
    [InlineData(StatusPedido.EmPreparacao, StatusPedido.Recebido)]
    [InlineData(StatusPedido.Cancelado, StatusPedido.Recebido)]
    [InlineData(StatusPedido.Pronto, StatusPedido.EmPreparacao)]
    [InlineData(StatusPedido.Finalizado, StatusPedido.Pronto)]
    public async Task Execute_StatusPermiteAlteracao_AtualizaPedido(StatusPedido novoStatus, StatusPedido statusAtual)
    {
        // Arrange
        var pedido = PedidoStubBuilder.NewBuilder()
            .WithPagamento(StatusPagamento.Autorizado)
            .WithStatus(statusAtual).Generate();
        var dto = new NovoStatusDePedidoDTO { PedidoId = pedido.Id, NovoStatus = novoStatus };
        
        _pedidoGatewayMock.Setup(pg => pg.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);
        _pedidoGatewayMock.Setup(pg => pg.UpdateAsync(pedido)).ReturnsAsync(pedido);

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.NotNull(result);
        _pedidoGatewayMock.Verify(pg => pg.UpdateAsync(pedido), Times.Once);
        Assert.Equal(novoStatus, result.StatusPedido);
    }
}

public class AtualizarStatusDePreparoPedidoTest : AtualizarStatusDePreparoPedidoUseCase
{
    public AtualizarStatusDePreparoPedidoTest(ILogger logger, IPedidoGateway pedidoGateway)
        : base(logger, pedidoGateway)
    {
    }

    public new Task<Pedido?> Execute(NovoStatusDePedidoDTO request)
    {
        return base.Execute(request);
    }
}