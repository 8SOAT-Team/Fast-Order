using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class ConfirmarPagamentoDTOTest
{
    [Fact]
    public void ConfirmarPagamentoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var status = StatusDoPagamento.Autorizado;

        // Act
        var dto = new ConfirmarPagamentoDTO(status);

        // Assert
        Assert.Equal(status, dto.Status);
    }
}