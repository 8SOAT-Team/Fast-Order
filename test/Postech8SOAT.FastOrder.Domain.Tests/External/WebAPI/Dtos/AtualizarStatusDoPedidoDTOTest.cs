using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class AtualizarStatusDoPedidoDTOTest
{
    [Fact]
    public void AtualizarStatusDoPedidoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var novoStatus = StatusPedido.Finalizado;

        // Act
        var dto = new AtualizarStatusDoPedidoDTO
        {
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(novoStatus, dto.NovoStatus);
    }
}