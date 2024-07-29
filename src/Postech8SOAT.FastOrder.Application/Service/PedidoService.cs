using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Service;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }


    private async Task<Pedido> DeveEncontrarPedido(Guid id)
    {
        var pedido = await _pedidoRepository.GetById(id);

        DomainExceptionValidation.When(pedido is null, "Pedido não encontrado");

        return pedido!;
    }

    private Task SalvarPedido(Pedido pedido) => _pedidoRepository.UpdateAsync(pedido);

    public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
    {
        await _pedidoRepository.AddAsync(pedido);
        return pedido;
    }

    public Task<List<Pedido>> GetAllPedidosAsync()
    {
        return _pedidoRepository.FindAllAsync();
    }

    public async Task<Pedido> GetPedidoByIdAsync(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);

        return pedido;
    }

    public async Task<Pedido> IniciarPreparo(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.IniciarPreparo();
        await SalvarPedido(pedido);
        return pedido;
    }

    public async Task<Pedido> FinalizarPreparo(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.FinalizarPreparo();
        await SalvarPedido(pedido);
        return pedido;
    }

    public async Task<Pedido> Entregar(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.Entregar();
        await SalvarPedido(pedido);
        return pedido;
    }

    public async Task<Pedido> Cancelar(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.Cancelar();
        await SalvarPedido(pedido);
        return pedido;
    }
}