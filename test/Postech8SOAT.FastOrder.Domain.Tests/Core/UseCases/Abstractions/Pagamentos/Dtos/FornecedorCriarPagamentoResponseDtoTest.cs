﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

public class FornecedorCriarPagamentoResponseDtoTest
{
    [Fact]
    public void FornecedorCriarPagamentoResponseDto_DeveValidarValores()
    {
        // Arrange
        var idExterno = "12345";
        var urlPagamento = "https://pagamento.exemplo.com";

        // Instanciando o DTO
        var responseDto = new FornecedorCriarPagamentoResponseDto(idExterno, urlPagamento);

        // Act & Assert
        Assert.Equal(idExterno, responseDto.IdExterno);
        Assert.Equal(urlPagamento, responseDto.UrlPagamento);
    }
}

