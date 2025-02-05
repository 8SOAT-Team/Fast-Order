using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Moq;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.UseCases.Pagamentos;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Pagamentos;

public sealed class IniciarPagamentoUseCaseTest
{
    private readonly Mock<ILogger> _mockLogger;
    private readonly Mock<IPedidoGateway> _mockPedidoGateway;
    private readonly Mock<IPagamentoGateway> _mockPagamentoGateway;
    private readonly Mock<IFornecedorPagamentoGateway> _mockFornecedorPagamentoGateway;
    private readonly IniciarPagamentoTest _useCase;

    public IniciarPagamentoUseCaseTest()
    {
        _mockLogger = new Mock<ILogger>();
        _mockPedidoGateway = new Mock<IPedidoGateway>();
        _mockPagamentoGateway = new Mock<IPagamentoGateway>();
        _mockFornecedorPagamentoGateway = new Mock<IFornecedorPagamentoGateway>();
        _useCase = new IniciarPagamentoTest(_mockLogger.Object, _mockPedidoGateway.Object, _mockPagamentoGateway.Object, _mockFornecedorPagamentoGateway.Object);
    }

    [Fact]
    public async Task Execute_DeveIniciarPagamentoQuandoPedidoValido()
    {
        // Arrange
        var pedido = PedidoStubBuilder.Create();
        var comando = new IniciarPagamentoDto(pedido.Id, MetodoDePagamento.Pix);

        _mockPedidoGateway.Setup(p => p.GetPedidoCompletoAsync(pedido.Id))
            .ReturnsAsync(pedido);

        _mockFornecedorPagamentoGateway.Setup(f => f.IniciarPagamento(
                comando.MetodoDePagamento,
                It.IsAny<string>(),
                pedido.ValorTotal,
                It.IsAny<string>(),
                pedido.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FornecedorCriarPagamentoResponseDto("12345", "http://linkdopagamento.com"));

        // Act
        var result = await _useCase.Execute(comando);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pedido.Pagamento, result);
        _mockPedidoGateway.Verify(p => p.AtualizarPedidoPagamentoIniciadoAsync(pedido), Times.Once);
    }

    [Fact]
    public async Task Execute_DeveRetornarErroQuandoPedidoNaoEncontrado()
    {
        // Arrange
        var comando = new IniciarPagamentoDto(Guid.NewGuid(), MetodoDePagamento.Master);

        _mockPedidoGateway.Setup(p => p.GetPedidoCompletoAsync(comando.PedidoId))
            .ReturnsAsync((Pedido?)null);

        // Act
        var result = await _useCase.Execute(comando);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Pedido não encontrado", useCaseErrors.FirstOrDefault().Description);
    }


    [Fact]
    public async Task Execute_DeveRetornarErroQuandoPagamentoJaIniciado()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var comando = new IniciarPagamentoDto(pedidoId, MetodoDePagamento.Master);

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var cliente = new Cliente(Guid.NewGuid(), "444.444.444-44", "nome cliente", "email@gmail.com");
        var pedido = new Pedido(pedidoId, cliente.Id, listaItens);

        var pagamento = new Pagamento(Guid.NewGuid(), pedido.Id, pedido, MetodoDePagamento.Master, 150.0m, "idExterno");
        pedido.IniciarPagamento(comando.MetodoDePagamento);

        _mockPedidoGateway.Setup(p => p.GetPedidoCompletoAsync(pedidoId))
            .ReturnsAsync(pedido);

        _mockFornecedorPagamentoGateway.Setup(f => f.IniciarPagamento(
                comando.MetodoDePagamento,
                cliente.Email,
                pedido.ValorTotal,
                It.IsAny<string>(),
                pedido.Id,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro no fornecedor de pagamento"));

        // Act
        var result = await _useCase.Execute(comando);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Pagamento já iniciado", useCaseErrors.FirstOrDefault().Description);
    }
}


public class IniciarPagamentoTest : IniciarPagamentoUseCase
{
    public IniciarPagamentoTest(ILogger logger,
        IPedidoGateway pedidoGateway,
        IPagamentoGateway pagamentoGateway,
        IFornecedorPagamentoGateway fornecedorPagamentoGateway)
        : base(logger, pedidoGateway, pagamentoGateway, fornecedorPagamentoGateway) { }

    public new Task<Pagamento?> Execute(IniciarPagamentoDto command)
    {
        return base.Execute(command);
    }

}