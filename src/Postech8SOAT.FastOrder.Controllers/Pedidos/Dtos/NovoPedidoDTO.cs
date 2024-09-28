using NovoPedidoUseCaseDto = Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos.NovoPedidoDTO;
using ItemDoPedidoUseCaseDto = Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos.ItemDoPedidoDTO;

namespace Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

public record NovoPedidoDTO
{
    public Guid? ClienteId { get; init; }
    public List<ItemDoPedidoDTO> ItensDoPedido { get; init; } = null!;


    public static implicit operator NovoPedidoUseCaseDto(NovoPedidoDTO novoPedidoDTO)
    {
        return new NovoPedidoUseCaseDto()
        {
            ClienteId = novoPedidoDTO.ClienteId,
            ItensDoPedido = novoPedidoDTO.ItensDoPedido.Select(i => (ItemDoPedidoUseCaseDto)i).ToList(),
        };
    }
}
