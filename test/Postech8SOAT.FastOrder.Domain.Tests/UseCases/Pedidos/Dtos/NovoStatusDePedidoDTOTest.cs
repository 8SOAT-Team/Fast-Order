using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pedidos.Dtos;
public class NovoStatusDePedidoDTOTest
{

    [Fact]
    public void DeveCriarNovoStatusDePedidoDTO_ComValoresCorretos()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Recebido;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDTO
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDTO_ComStatusCancelado()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Cancelado;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDTO
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDTO_ComStatusConcluido()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Finalizado;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDTO
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDTO_ComStatusEmProcessamento()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.EmPreparacao;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDTO
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

}
