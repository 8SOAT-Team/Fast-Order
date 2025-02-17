using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

public class FornecedorGetPagamentoResponseDtoTest
{
    [Fact]
    public void FornecedorGetPagamentoResponseDto_DeveValidarValores()
    {
        // Arrange
        var idExterno = "12345";
        var pagamentoId = Guid.NewGuid();
        var statusPagamento = StatusPagamento.Autorizado;

        // Instanciando o DTO
        var responseDto = new FornecedorGetPagamentoResponseDto(idExterno, pagamentoId, statusPagamento);

        // Act & Assert
        Assert.Equal(idExterno, responseDto.IdExterno);
        Assert.Equal(pagamentoId, responseDto.PagamentoId);
        Assert.Equal(statusPagamento, responseDto.StatusPagamento);
    }
}
