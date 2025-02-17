using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Presenters.Pagamentos;

public class PagamentoPresenterTest
{
    [Fact]
    public void ToPagamentoDTO_ShouldReturnCorrectDto()
    {
        // Arrange
        var pagamento = PagamentoStubBuilder.Create();

        // Act
        var result = PagamentoPresenter.ToPagamentoDTO(pagamento);

        // Assert
        Assert.Equal(pagamento.Id, result.Id);
        Assert.Equal((MetodosDePagamento)pagamento.MetodoDePagamento, result.MetodoDePagamento);
        Assert.Equal((StatusDoPagamento)pagamento.Status, result.status);
        Assert.Equal(pagamento.ValorTotal, result.ValorTotal);
        Assert.Equal(pagamento.PagamentoExternoId, result.PagamentoExternoId);
        Assert.Equal(pagamento.UrlPagamento, result.UrlPagamento);
    }

    [Fact]
    public void ToListPagamentoDTO_ShouldReturnCorrectDtoList()
    {
        // Arrange
        var pagamentos = new List<Pagamento>
        {
            PagamentoStubBuilder.Create(),
            PagamentoStubBuilder.Create()
        };

        // Act
        var result = PagamentoPresenter.ToListPagamentoDTO(pagamentos);

        // Assert
        Assert.Equal(pagamentos.Count, result.Count);
        for (int i = 0; i < pagamentos.Count; i++)
        {
            Assert.Equal(pagamentos[i].Id, result[i].Id);
            Assert.Equal((MetodosDePagamento)pagamentos[i].MetodoDePagamento, result[i].MetodoDePagamento);
            Assert.Equal((StatusDoPagamento)pagamentos[i].Status, result[i].status);
            Assert.Equal(pagamentos[i].ValorTotal, result[i].ValorTotal);
            Assert.Equal(pagamentos[i].PagamentoExternoId, result[i].PagamentoExternoId);
            Assert.Equal(pagamentos[i].UrlPagamento, result[i].UrlPagamento);
        }
    }
}