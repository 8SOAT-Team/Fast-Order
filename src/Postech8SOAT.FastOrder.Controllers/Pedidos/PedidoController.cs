using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pedidos;
using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Controllers.Problems;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Service;
using System.Collections.Generic;

namespace Postech8SOAT.FastOrder.Controllers.Pedidos;
public class PedidoController : IPedidoController
{
    private readonly IPedidoUseCase pedidoUseCase;
    private readonly IPedidoServiceUseCaseInvoker commandInvoker;
    private readonly ILogger _logger;
    private readonly IPedidoGateway _pedidoGateway;

    public PedidoController(IPedidoUseCase pedidoUseCase, ILogger logger, IPedidoGateway pedidoGateway
        , IPedidoServiceUseCaseInvoker commandInvoker)
    {
        this.pedidoUseCase = pedidoUseCase;
        this._logger = logger;
        _pedidoGateway = pedidoGateway;
        this.commandInvoker = commandInvoker;
    }

    public Task<Pedido> Cancelar(Guid id)
    {
        return pedidoUseCase.Cancelar(id);
    }

    public async Task<Result<PedidoCriadoDTO>> CreatePedidoAsync(NovoPedidoDTO novoPedido)
    {
        var useCase = new CriarNovoPedidoUseCase(_logger, _pedidoGateway);
        var result = await useCase.ResolveAsync(novoPedido);

        if (useCase.IsFailure)
        {
            return Result<PedidoCriadoDTO>.Failure(useCase.GetErrors().AdaptUseCaseErrors().ToList());
        }

        if(result.HasValue is false)
        {
            return Result<PedidoCriadoDTO>.Empty();
        }

       var pedido = await _pedidoGateway.GetPedidoCompletoAsync(result.Value!.Id);

        return Result<PedidoCriadoDTO>.Succeed(PedidoPresenter.ToPedidoCriadoDTO(pedido!));
       
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

    public async Task<Result<List<Pedido>>> GetAllPedidosShowStatusAsync()
    {
        var useCase = new PedidoUseCase(_pedidoGateway);
        var result = await useCase.GetAllPedidosShowStatusAsync();
        return Result<List<Pedido>>.Succeed(result);
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
