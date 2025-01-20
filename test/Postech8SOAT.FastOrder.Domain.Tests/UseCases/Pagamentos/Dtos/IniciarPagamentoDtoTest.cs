using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pagamentos.Dtos;
public class IniciarPagamentoDtoTest
{
    [Fact]
    public void DeveCriarIniciarPagamentoDtoCorretamente()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var metodoPagamento = MetodoDePagamento.Master;

        // Act
        var dto = new IniciarPagamentoDto(pedidoId, metodoPagamento);

        // Assert
        Assert.Equal(pedidoId, dto.PedidoId);
        Assert.Equal(metodoPagamento, dto.MetodoDePagamento);
    }

    [Fact]
    public void DeveSerIgualQuandoPropriedadesSaoIguais()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var metodoPagamento = MetodoDePagamento.Master;

        var dto1 = new IniciarPagamentoDto(pedidoId, metodoPagamento);
        var dto2 = new IniciarPagamentoDto(pedidoId, metodoPagamento);

        // Act & Assert
        Assert.Equal(dto1, dto2); 
    }

    [Fact]
    public void DeveSerDiferenteQuandoPropriedadesSaoDiferentes()
    {
        // Arrange
        var pedidoId1 = Guid.NewGuid();
        var pedidoId2 = Guid.NewGuid();
        var metodoPagamento = MetodoDePagamento.Master;

        var dto1 = new IniciarPagamentoDto(pedidoId1, metodoPagamento);
        var dto2 = new IniciarPagamentoDto(pedidoId2, metodoPagamento);

        // Act & Assert
        Assert.NotEqual(dto1, dto2); 
    }

    [Fact]
    public void DevePermitirDesestruturacao()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var metodoPagamento = MetodoDePagamento.Master;
        var dto = new IniciarPagamentoDto(pedidoId, metodoPagamento);

        // Act
        var (pedidoIdDesestruturado, metodoPagamentoDesestruturado) = dto;

        // Assert
        Assert.Equal(pedidoId, pedidoIdDesestruturado);
        Assert.Equal(metodoPagamento, metodoPagamentoDesestruturado);
    }


}
