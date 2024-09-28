namespace Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

public record ClienteDTO
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Cpf { get; init; } = null!;
}