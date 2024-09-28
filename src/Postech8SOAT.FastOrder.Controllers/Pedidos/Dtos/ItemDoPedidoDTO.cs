using ItemDoPedidoUseCaseDto = Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos.ItemDoPedidoDTO;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

public record ItemDoPedidoDTO
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
    public string Imagem { get; private set; } = null!;


    public static implicit operator ItemDoPedidoUseCaseDto(ItemDoPedidoDTO itemDoPedidoDTO)
    {
        return new ItemDoPedidoUseCaseDto()
        {
            ProdutoId = itemDoPedidoDTO.ProdutoId,
            Quantidade = itemDoPedidoDTO.Quantidade,
        };
    }

    public static implicit operator ItemDoPedidoDTO(ItemDoPedido itemDoPedido)
    {
        return new ItemDoPedidoDTO
        {
            Id = itemDoPedido.Id,
            ProdutoId = itemDoPedido.ProdutoId,
            Quantidade = itemDoPedido.Quantidade,
            Imagem = itemDoPedido.Produto?.Imagem!,
        };
    }
}
