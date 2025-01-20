using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Clientes.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Abstractions.Clientes.Dtos;
public class CriarNovoClienteDtoTest
{
    [Fact]
    public void CriarNovoClienteDto_DeveValidarValores()
    {
        // Arrange
        var cpf = new Cpf("12345678901");
        var nome = "João Silva";
        var email = new EmailAddress("joao.silva@example.com");

        // Instanciando o DTO
        var clienteDto = new CriarNovoClienteDto(cpf, nome, email);

        // Act & Assert
        Assert.Equal(cpf, clienteDto.Cpf);
        Assert.Equal(nome, clienteDto.Nome);
        Assert.Equal(email, clienteDto.Email);
    }
}
