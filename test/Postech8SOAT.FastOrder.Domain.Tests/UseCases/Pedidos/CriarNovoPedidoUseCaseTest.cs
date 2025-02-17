using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

public class CriarNovoPedidoUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly CriarNovoPedidoTest _useCase;

    public CriarNovoPedidoUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new CriarNovoPedidoTest(_loggerMock.Object, _pedidoGatewayMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_ProdutosNaoEncontrados_RetornaErro()
    {
        // Arrange
        var produtoIdInexistente = Guid.NewGuid();
        var dto = new NovoPedidoDTO
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = new List<ItemDoPedidoDTO>
                {
                    new ItemDoPedidoDTO { ProdutoId = produtoIdInexistente, Quantidade = 1 }
                }
        };


        _produtoGatewayMock.Setup(pg => pg.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto>());

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Produto não encontrado: " + produtoIdInexistente, useCaseErrors.FirstOrDefault().Description);
    }

    [Fact]
    public async Task Execute_ProdutosEncontrados_RetornaPedidoCriado()
    {
        // Arrange
        var produtoIdExistente = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var produto = new Produto(produtoIdExistente, "Lanche", "Lanche de bacon", 10.0m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = new List<ItemDoPedidoDTO>
                {
                    new ItemDoPedidoDTO { ProdutoId = produtoIdExistente, Quantidade = 1 }
                }
        };

        _produtoGatewayMock.Setup(pg => pg.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto> { produto });

        var pedidoCriado = new Pedido(Guid.NewGuid(), clienteId, new List<ItemDoPedido> {
            new ItemDoPedido(Guid.NewGuid(), produto, 1)
        });
        _pedidoGatewayMock.Setup(pg => pg.CreateAsync(It.IsAny<Pedido>())).ReturnsAsync(pedidoCriado);

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Pedido>(result);
        Assert.Equal(clienteId, result.ClienteId);
        _pedidoGatewayMock.Verify(pg => pg.CreateAsync(It.IsAny<Pedido>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGatewayDePedido_RetornaErro()
    {
        // Arrange
        var produtoIdExistente = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var produto = new Produto(produtoIdExistente, "Lanche", "Lanche de bacon", 10.0m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = new List<ItemDoPedidoDTO>
                {
                    new ItemDoPedidoDTO { ProdutoId = produtoIdExistente, Quantidade = 1 }
                }
        };

        _produtoGatewayMock.Setup(pg => pg.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto> { produto });
        _pedidoGatewayMock.Setup(pg => pg.CreateAsync(It.IsAny<Pedido>())).ThrowsAsync(new Exception("Erro ao criar pedido"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(dto));

        // Assert
        Assert.Equal("Erro ao criar pedido", exception.Message);
    }

    [Fact]
    public async Task Execute_ProdutosDiferentes_RetornaErroProdutoNaoEncontrado()
    {
        // Arrange
        var produtoIdExistente = Guid.NewGuid();
        var produtoIdInexistente = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var produto = new Produto(produtoIdExistente, "Lanche", "Lanche de bacon", 10.0m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = new List<ItemDoPedidoDTO>
                {
                    new ItemDoPedidoDTO { ProdutoId = produtoIdExistente, Quantidade = 1 },
                    new ItemDoPedidoDTO { ProdutoId = produtoIdInexistente, Quantidade = 1 }
                }
        };

        _produtoGatewayMock.Setup(pg => pg.ListarProdutosByIdAsync(It.IsAny<Guid[]>()))
            .ReturnsAsync(new List<Produto> { produto });

        // Act
        var result = await _useCase.Execute(dto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Produto não encontrado: " + produtoIdInexistente, useCaseErrors.FirstOrDefault().Description);
    }
}

public class CriarNovoPedidoTest : CriarNovoPedidoUseCase
{
    public CriarNovoPedidoTest(ILogger logger,
    IPedidoGateway pedidoGateway,
    IProdutoGateway produtoGateway)
        : base(logger, pedidoGateway, produtoGateway) { }

    public new Task<Pedido?> Execute(NovoPedidoDTO command)
    {
        return base.Execute(command);
    }

}