using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Pagamentos.Dtos;
public class ConfirmarPagamentoDtoTest
{
    [Fact]
    public void DeveCriarConfirmarPagamentoDtoCorretamente()
    {
        // Arrange
        var pagamentoId = Guid.NewGuid();
        var statusPagamento = StatusPagamento.Autorizado;

        // Act
        var dto = new ConfirmarPagamentoDto(pagamentoId, statusPagamento);

        // Assert
        Assert.Equal(pagamentoId, dto.PagamentoId);
        Assert.Equal(statusPagamento, dto.Status);
    }

    [Fact]
    public void DeveSerIgualQuandoPropriedadesSaoIguais()
    {
        // Arrange
        var pagamentoId = Guid.NewGuid();
        var statusPagamento = StatusPagamento.Autorizado;

        var dto1 = new ConfirmarPagamentoDto(pagamentoId, statusPagamento);
        var dto2 = new ConfirmarPagamentoDto(pagamentoId, statusPagamento);

        // Act & Assert
        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void DeveSerDiferenteQuandoPropriedadesSaoDiferentes()
    {
        // Arrange
        var pagamentoId1 = Guid.NewGuid();
        var pagamentoId2 = Guid.NewGuid();
        var statusPagamento = StatusPagamento.Autorizado;

        var dto1 = new ConfirmarPagamentoDto(pagamentoId1, statusPagamento);
        var dto2 = new ConfirmarPagamentoDto(pagamentoId2, statusPagamento);

        // Act & Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void DevePermitirDesestruturacao()
    {
        // Arrange
        var pagamentoId = Guid.NewGuid();
        var statusPagamento = StatusPagamento.Autorizado;
        var dto = new ConfirmarPagamentoDto(pagamentoId, statusPagamento);

        // Act
        var (pagamentoIdDesestruturado, statusPagamentoDesestruturado) = dto;

        // Assert
        Assert.Equal(pagamentoId, pagamentoIdDesestruturado);
        Assert.Equal(statusPagamento, statusPagamentoDesestruturado);
    }


}
