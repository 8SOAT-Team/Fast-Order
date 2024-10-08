namespace Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

public record ItemDoPedidoDTO
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
    public string Imagem { get; init; } = null!;
}
