using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pagamentos;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;
using CleanArch.UseCase.Faults;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pagamentos;
public class ConfirmarPagamentoUseCaseTest
{

    [Fact]
    public async Task Execute_DeveAdicionarErroQuandoPagamentoNaoEncontrado()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPagamentoGateway = new Mock<IPagamentoGateway>();
        var mockPedidoGateway = new Mock<IPedidoGateway>();

        var pagamentoId = Guid.NewGuid();
        var dto = new ConfirmarPagamentoDto(pagamentoId, StatusPagamento.Autorizado);

        mockPagamentoGateway.Setup(g => g.GetByIdAsync(pagamentoId)).ReturnsAsync((Pagamento?)null); 

        var useCase = new ConfirmarPagamentoTest(mockLogger.Object, mockPagamentoGateway.Object, mockPedidoGateway.Object);

        // Act
        var resultado = await useCase.Execute(dto);

        // Assert
        Assert.Null(resultado);
        IReadOnlyCollection<UseCaseError> useCaseErrors = useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Pagamento não encontrado", useCaseErrors.FirstOrDefault().Description);
    }

    [Fact]
    public async Task Execute_DeveAdicionarErroQuandoPedidoNaoEncontrado()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPagamentoGateway = new Mock<IPagamentoGateway>();
        var mockPedidoGateway = new Mock<IPedidoGateway>();

        var pagamentoId = Guid.NewGuid();
        var pedidoId = Guid.NewGuid();
        var dto = new ConfirmarPagamentoDto(pagamentoId, StatusPagamento.Autorizado);

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(pedidoId, Guid.NewGuid(), listaItens);

        var pagamento = new Pagamento(pagamentoId, pedido.Id, pedido, MetodoDePagamento.Pix, 100m, "idExterno");

        mockPagamentoGateway.Setup(g => g.GetByIdAsync(pagamentoId)).ReturnsAsync(pagamento); 
        mockPedidoGateway.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido?)null); 

        var useCase = new ConfirmarPagamentoTest(mockLogger.Object, mockPagamentoGateway.Object, mockPedidoGateway.Object);

        // Act
        var resultado = await useCase.Execute(dto);

        // Assert
        Assert.Null(resultado);
        IReadOnlyCollection<UseCaseError> useCaseErrors = useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Pedido não encontrado", useCaseErrors.FirstOrDefault().Description);
    }

    [Fact]
    public async Task Execute_DeveIniciarPreparoQuandoPagamentoAutorizado()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPagamentoGateway = new Mock<IPagamentoGateway>();
        var mockPedidoGateway = new Mock<IPedidoGateway>();

        var pagamentoId = Guid.NewGuid();
        var pedidoId = Guid.NewGuid();
        var dto = new ConfirmarPagamentoDto(pagamentoId, StatusPagamento.Autorizado);

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(pedidoId, Guid.NewGuid(), listaItens);

        var pagamento = new Pagamento(pagamentoId, pedido.Id, pedido, MetodoDePagamento.Pix, 100m, "idExterno");

        mockPagamentoGateway.Setup(g => g.GetByIdAsync(pagamentoId)).ReturnsAsync(pagamento); 
        mockPedidoGateway.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync(pedido); 
        mockPagamentoGateway.Setup(g => g.UpdatePagamentoAsync(It.IsAny<Pagamento>())).ReturnsAsync(pagamento);

        var useCase = new ConfirmarPagamentoTest(mockLogger.Object, mockPagamentoGateway.Object, mockPedidoGateway.Object);

        // Act
        var resultado = await useCase.Execute(dto);

        // Assert
        Assert.NotNull(resultado); 
        mockPedidoGateway.Verify(g => g.UpdateAsync(It.IsAny<Pedido>()), Times.Once); 
    }

    [Fact]
    public async Task Execute_NaoDeveAlterarPedidoQuandoPagamentoNaoAutorizado()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var mockPagamentoGateway = new Mock<IPagamentoGateway>();
        var mockPedidoGateway = new Mock<IPedidoGateway>();

        var pagamentoId = Guid.NewGuid();
        var pedidoId = Guid.NewGuid();
        var dto = new ConfirmarPagamentoDto(pagamentoId, StatusPagamento.Rejeitado);

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(pedidoId, Guid.NewGuid(), listaItens);

        var pagamento = new Pagamento(pagamentoId, pedido.Id, pedido, MetodoDePagamento.Pix, 100m, "idExterno");

        mockPagamentoGateway.Setup(g => g.GetByIdAsync(pagamentoId)).ReturnsAsync(pagamento);
        mockPedidoGateway.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync(pedido); 
        mockPagamentoGateway.Setup(g => g.UpdatePagamentoAsync(It.IsAny<Pagamento>())).ReturnsAsync(pagamento); 

        var useCase = new ConfirmarPagamentoTest(mockLogger.Object, mockPagamentoGateway.Object, mockPedidoGateway.Object);

        // Act
        var resultado = await useCase.Execute(dto);

        // Assert
        Assert.NotNull(resultado); 
        mockPedidoGateway.Verify(g => g.UpdateAsync(It.IsAny<Pedido>()), Times.Never); 
    }

}

public class ConfirmarPagamentoTest : ConfirmarPagamentoUseCase
{
    public ConfirmarPagamentoTest(ILogger logger, IPagamentoGateway pagamentoGateway, IPedidoGateway pedidoGateway)
        : base(logger, pagamentoGateway, pedidoGateway) { }

    public new Task<Pagamento?> Execute(ConfirmarPagamentoDto dto) 
    {
        return base.Execute(dto);
    }

}

