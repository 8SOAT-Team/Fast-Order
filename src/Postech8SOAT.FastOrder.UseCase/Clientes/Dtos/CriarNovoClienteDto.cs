using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.UseCases.Clientes.Dtos;

public record CriarNovoClienteDto(Cpf Cpf, string Nome, EmailAddress Email);
