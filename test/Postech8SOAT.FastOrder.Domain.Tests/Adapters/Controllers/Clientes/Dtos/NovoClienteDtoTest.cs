using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Clientes.Dtos
{
    public class NovoClienteDtoTest
    {
        [Fact]
        public void NovoClienteDto_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var cpf = "12345678900";
            var nome = "Test Name";
            var email = "test@example.com";

            // Act
            var dto = new NovoClienteDto(cpf, nome, email);

            // Assert
            Assert.Equal(cpf, dto.Cpf);
            Assert.Equal(nome, dto.Nome);
            Assert.Equal(email, dto.Email);
        }

        [Fact]
        public void NovoClienteDto_ShouldHaveCorrectDefaultValues()
        {
            // Act
            var dto = new NovoClienteDto(default, default, default);

            // Assert
            Assert.Equal(default(string), dto.Cpf);
            Assert.Equal(default(string), dto.Nome);
            Assert.Equal(default(string), dto.Email);
        }
    }
}