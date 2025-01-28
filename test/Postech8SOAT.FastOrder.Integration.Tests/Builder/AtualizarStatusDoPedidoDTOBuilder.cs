using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class AtualizarStatusDoPedidoDTOBuilder:Faker<AtualizarStatusDoPedidoDTO>
{
    public AtualizarStatusDoPedidoDTOBuilder()
    {
        CustomInstantiator(f => new AtualizarStatusDoPedidoDTO()
        {
            NovoStatus = StatusPedido.EmPreparacao
        });
    }
    public AtualizarStatusDoPedidoDTO Build() => Generate();
}
