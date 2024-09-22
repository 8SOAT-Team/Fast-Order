using Azure.Core;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class PedidoController:IPedidoController
{
    private readonly IPedidoUseCase pedidoUseCase;
    private readonly IPedidoServiceUseCaseInvoker commandInvoker;

    public PedidoController(IPedidoUseCase pedidoUseCase)
    {
        this.pedidoUseCase = pedidoUseCase;
    }

    public Task<Pedido> Cancelar(Guid id)
    {
        return pedidoUseCase.Cancelar(id);
    }

    public Task<Pedido> CreatePedidoAsync(Pedido pedido)
    {
        return pedidoUseCase.CreatePedidoAsync(pedido);
    }

    public Task<Pedido> Entregar(Guid id)
    {
        return pedidoUseCase.Entregar(id);
    }

    public Task<Pedido> FinalizarPreparo(Guid id)
    {
        return pedidoUseCase.FinalizarPreparo(id);
    }

    public Task<Pedido> GetPedidoByIdAsync(Guid id)
    {
        return pedidoUseCase.GetPedidoByIdAsync(id);
    }

    public Task<List<Pedido>> GetAllPedidosAsync()
    {
        return pedidoUseCase.GetAllPedidosAsync();
    }

    public Task<Pedido> IniciarPreparo(Guid id)
    {
        return pedidoUseCase.IniciarPreparo(id);
    }

    public async Task AtualizaStatus(StatusPedido status, Guid pedidoId)
    {
        await commandInvoker.ExecutarComandoAsync(status, pedidoId, pedidoUseCase);
    }
}
