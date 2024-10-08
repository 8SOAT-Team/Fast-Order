using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;
using NovoPedidoDTO = Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos.NovoPedidoDTO;
using UseCaseNovoPedidoDTO = Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos.NovoPedidoDTO;
using ItemDoPedidoDTO = Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos.ItemDoPedidoDTO;

namespace Postech8SOAT.FastOrder.Controllers.Pedidos;
public class PedidoController(
    ILogger logger,
    IPedidoGateway pedidoGateway,
    IProdutoGateway produtoGateway) : IPedidoController
{
    private readonly ILogger _logger = logger;
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;
    private readonly IProdutoGateway _produtoGateway = produtoGateway;

    public async Task<Result<PedidoDTO>> AtualizarStatusDePreparacaoDoPedido(StatusPedido novoStatus, Guid pedidoId)
    {
        var useCase = new AtualizarStatusDePreparoPedidoUseCase(_logger, _pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(new NovoStatusDePedidoDTO()
        {
            NovoStatus = novoStatus,
            PedidoId = pedidoId
        });

        return ControllerResultBuilder<PedidoDTO, Pedido>
           .ForUseCase(useCase)
           .WithInstance(pedidoId)
           .WithResult(useCaseResult)
           .AdaptUsing(PedidoPresenter.ToPedidoDTO)
           .Build();
    }

    public async Task<Result<PedidoDTO>> CreatePedidoAsync(NovoPedidoDTO pedido)
    {
        var useCase = new CriarNovoPedidoUseCase(_logger, _pedidoGateway, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(new UseCaseNovoPedidoDTO()
        {
            ClienteId = pedido.ClienteId,
            ItensDoPedido = pedido.ItensDoPedido.Select(i => new ItemDoPedidoDTO()
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        });

        return ControllerResultBuilder<PedidoDTO, Pedido>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToPedidoDTO)
            .Build();
    }

    public async Task<Result<List<PedidoDTO>>> GetAllPedidosAsync()
    {
        var useCase = new ListarTodosPedidosUseCase(_logger, _pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(Any<object>.Empty);

        return ControllerResultBuilder<List<PedidoDTO>, List<Pedido>>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToListPedidoDTO)
            .Build();
    }

    public async Task<Result<List<PedidoDTO>>> GetAllPedidosPending()
    {
        var useCase = new ObterListaPedidosPendentesUseCase(_logger, _pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(Any<object>.Empty);

        return ControllerResultBuilder<List<PedidoDTO>, List<Pedido>>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToListPedidoDTO)
            .Build();
    }

    public async Task<Result<PedidoDTO>> GetPedidoByIdAsync(Guid id)
    {
        var useCase = new EncontrarPedidoPorIdUseCase(_logger, _pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(id);

        return ControllerResultBuilder<PedidoDTO, Pedido>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToPedidoDTO)
            .Build();
    }
}
