using System.Reflection.Metadata.Ecma335;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record ItemDoPedidoDTO
{
    public Guid Id { get; private set; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
    public void SetId(Guid id) => Id = id;
}