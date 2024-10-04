using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;

public class PedidoUseCase : IPedidoUseCase
{
    private readonly IPedidoGateway _pedidoGateway;

    public PedidoUseCase(IPedidoGateway pedidoGateway)
    {
        _pedidoGateway = pedidoGateway;
    }


    private async Task<Pedido> DeveEncontrarPedido(Guid id)
    {
        var pedido = await _pedidoGateway.GetPedidoByIdAsync(id);

        DomainExceptionValidation.When(pedido is null, "Pedido não encontrado");

        return pedido!;
    }

    public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
    {
        await _pedidoGateway.CreatePedidoAsync(pedido);
        return pedido;
    }

    public Task<List<Pedido>> GetAllPedidosAsync()
    {
        return _pedidoGateway.GetAllPedidosAsync();
    }
    
    public Task<List<Pedido>> GetAllPedidosShowStatusAsync()
    {
        return _pedidoGateway.GetAllPedidosShowStatusAsync();
    }

    public async Task<Pedido> GetPedidoByIdAsync(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);

        return pedido;
    }

    public async Task<Pedido> IniciarPreparo(Guid id)
    {
        return await _pedidoGateway.IniciarPreparo(id);
    }

    public async Task<Pedido> FinalizarPreparo(Guid id)
    {
        return await _pedidoGateway.FinalizarPreparo(id);
    }

    public async Task<Pedido> Entregar(Guid id)
    {
       return await _pedidoGateway.Entregar(id);
    }

    public async Task<Pedido> Cancelar(Guid id)
    {
        return await _pedidoGateway.Cancelar(id);
    }
}