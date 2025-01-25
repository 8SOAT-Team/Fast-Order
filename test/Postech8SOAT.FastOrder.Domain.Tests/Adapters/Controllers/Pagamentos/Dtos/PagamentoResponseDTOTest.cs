using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Pagamentos.Dtos;

public class PagamentoResponseDTOTest
{
    [Fact]
    public void PagamentoResponseDTO_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var metodoDePagamento = MetodosDePagamento.Master;
        var status = StatusDoPagamento.Autorizado;
        var valorTotal = 100.50m;
        var pagamentoExternoId = "external123";
        var urlPagamento = "http://payment.url";

        // Act
        var dto = new PagamentoResponseDTO(id, metodoDePagamento, status, valorTotal, pagamentoExternoId, urlPagamento);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(metodoDePagamento, dto.MetodoDePagamento);
        Assert.Equal(status, dto.status);
        Assert.Equal(valorTotal, dto.ValorTotal);
        Assert.Equal(pagamentoExternoId, dto.PagamentoExternoId);
        Assert.Equal(urlPagamento, dto.UrlPagamento);
    }

    [Fact]
    public void PagamentoResponseDTO_ShouldHandleNullUrlPagamento()
    {
        // Arrange
        var id = Guid.NewGuid();
        var metodoDePagamento = MetodosDePagamento.Master;
        var status = StatusDoPagamento.Autorizado;
        var valorTotal = 100.50m;
        var pagamentoExternoId = "external123";
        string? urlPagamento = null;

        // Act
        var dto = new PagamentoResponseDTO(id, metodoDePagamento, status, valorTotal, pagamentoExternoId, urlPagamento);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(metodoDePagamento, dto.MetodoDePagamento);
        Assert.Equal(status, dto.status);
        Assert.Equal(valorTotal, dto.ValorTotal);
        Assert.Equal(pagamentoExternoId, dto.PagamentoExternoId);
        Assert.Null(dto.UrlPagamento);
    }
}