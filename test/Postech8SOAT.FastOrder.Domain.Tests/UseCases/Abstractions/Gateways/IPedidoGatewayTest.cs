using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Moq;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Abstractions.Gateways;
public class IPedidoGatewayTest
{

    [Fact]
    public async Task BuscarPedidoPorIdAsync_DeveRetornarPedidoQuandoExistir()
    {
        // Arrange
        var mockPedidoGateway = new Mock<IPedidoGateway>();
        var id = Guid.NewGuid();
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(id, Guid.NewGuid(), listaItens);
        mockPedidoGateway.Setup(gateway => gateway.GetByIdAsync(id))
            .ReturnsAsync(pedido);

        var pedidoService = new PedidoServiceTest(mockPedidoGateway.Object);

        // Act
        var pedidoRetornado = await pedidoService.BuscarPedidoPorIdAsync(id);

        // Assert
        Assert.NotNull(pedidoRetornado);
        Assert.Equal(id, pedidoRetornado?.Id);
    }

    [Fact]
    public async Task CriarPedidoAsync_DeveCriarNovoPedido()
    {
        // Arrange
        var mockPedidoGateway = new Mock<IPedidoGateway>();
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
        listaItens.Add(itemPedido);
        var pedido = new Pedido(Guid.NewGuid(), listaItens);
        mockPedidoGateway.Setup(gateway => gateway.CreateAsync(pedido))
            .ReturnsAsync(pedido);

        var pedidoService = new PedidoServiceTest(mockPedidoGateway.Object);

        // Act
        var pedidoCriado = await pedidoService.CriarPedidoAsync(pedido);

        // Assert
        Assert.NotNull(pedidoCriado);
        Assert.Equal(pedido.Id, pedidoCriado.Id);
    }

    [Fact]
    public async Task AtualizarPedidoAsync_DeveAtualizarPedidoExistente()
    {
        // Arrange
        var mockPedidoGateway = new Mock<IPedidoGateway>();
        var id = Guid.NewGuid();
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido = new Pedido(id, Guid.NewGuid(), listaItens);
        mockPedidoGateway.Setup(gateway => gateway.UpdateAsync(pedido))
            .ReturnsAsync(pedido);

        var pedidoService = new PedidoServiceTest(mockPedidoGateway.Object);

        // Act
        var pedidoAtualizado = await pedidoService.AtualizarPedidoAsync(pedido);

        // Assert
        Assert.NotNull(pedidoAtualizado);
        Assert.Equal(id, pedidoAtualizado.Id);
    }

    [Fact]
    public async Task BuscarPedidosPendentesAsync_DeveRetornarPedidosPendentes()
    {
        // Arrange
        var mockPedidoGateway = new Mock<IPedidoGateway>();

        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        List<ItemDoPedido> listaItens = new List<ItemDoPedido> { itemPedido };
        var pedido1 = new Pedido(Guid.NewGuid(), listaItens);

        var produto2 = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var itemPedido2 = new ItemDoPedido(Guid.NewGuid(), produto2, 2);
        List<ItemDoPedido> listaItens2 = new List<ItemDoPedido> { itemPedido };
        var pedido2 = new Pedido(Guid.NewGuid(), listaItens2);

        mockPedidoGateway.Setup(gateway => gateway.GetAllPedidosPending())
            .ReturnsAsync(new List<Pedido> { pedido1 });

        var pedidoService = new PedidoServiceTest(mockPedidoGateway.Object);

        // Act
        var pedidosPendentes = await pedidoService.BuscarPedidosPendentesAsync();

        // Assert
        Assert.NotNull(pedidosPendentes);
        Assert.Single(pedidosPendentes);
    }

}

public class PedidoServiceTest
{
    private readonly IPedidoGateway _pedidoGateway;

    public PedidoServiceTest(IPedidoGateway pedidoGateway)
    {
        _pedidoGateway = pedidoGateway;
    }

    public async Task<Pedido?> BuscarPedidoPorIdAsync(Guid id)
    {
        return await _pedidoGateway.GetByIdAsync(id);
    }

    public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
    {
        return await _pedidoGateway.CreateAsync(pedido);
    }

    public async Task<Pedido> AtualizarPedidoAsync(Pedido pedido)
    {
        return await _pedidoGateway.UpdateAsync(pedido);
    }

    public async Task<List<Pedido>> BuscarTodosPedidosAsync()
    {
        return await _pedidoGateway.GetAllAsync();
    }

    public async Task<List<Pedido>> BuscarPedidosPendentesAsync()
    {
        return await _pedidoGateway.GetAllPedidosPending();
    }

    public async Task<Pedido?> BuscarPedidoCompletoAsync(Guid id)
    {
        return await _pedidoGateway.GetPedidoCompletoAsync(id);
    }

    public async Task<Pedido> AtualizarStatusPagamentoAsync(Pedido pedido)
    {
        return await _pedidoGateway.AtualizarPedidoPagamentoIniciadoAsync(pedido);
    }
}

