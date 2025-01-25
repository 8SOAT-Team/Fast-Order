using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class NovoPagamentoDTOTest
{
    [Fact]
    public void NovoPagamentoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var metodoDePagamento = MetodosDePagamento.Master;

        // Act
        var dto = new NovoPagamentoDTO
        {
            MetodoDePagamento = metodoDePagamento
        };

        // Assert
        Assert.Equal(metodoDePagamento, dto.MetodoDePagamento);
    }
}