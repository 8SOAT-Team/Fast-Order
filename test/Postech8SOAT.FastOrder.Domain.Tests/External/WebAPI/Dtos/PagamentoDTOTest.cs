using System.ComponentModel.DataAnnotations;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class PagamentoDTOTest
{
    [Fact]
    public void PagamentoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pedidoId = Guid.NewGuid();
        var pagamentoExternoId = "12345";
        var status = StatusPagamento.Autorizado;
        var metodoDePagamento = MetodoDePagamento.Master;
        var valorTotal = 100.50m;

        // Act
        var dto = PagamentoDTO.Create(id, pedidoId, pagamentoExternoId, status, metodoDePagamento, valorTotal);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(pedidoId, dto.PedidoId);
        Assert.Equal(pagamentoExternoId, dto.PagamentoExternoId);
        Assert.Equal(status, dto.Status);
        Assert.Equal(metodoDePagamento, dto.MetodoDePagamento);
        Assert.Equal(valorTotal, dto.ValorTotal);
    }

    [Fact]
    public void PagamentoDTO_ShouldRequireMetodoDePagamento()
    {
        // Arrange
        var dto = new PagamentoDTO(MetodoDePagamento.Master);

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.True(isValid);
    }
}