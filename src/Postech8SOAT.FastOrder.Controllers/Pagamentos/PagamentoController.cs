using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Pagamentos;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Pagamentos;

public class PagamentoController(
    ILogger logger,
    IPedidoGateway pedidoGateway,
    IPagamentoGateway pagamentoGateway,
    IFornecedorPagamentoGateway fornecedorPagamentoGateway) : IPagamentoController
{
    private readonly ILogger _logger = logger;
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;
    private readonly IFornecedorPagamentoGateway _fornecedorPagamentoGateway = fornecedorPagamentoGateway;

    public async Task<Result<PagamentoResponseDTO>> ConfirmarPagamento(Guid pagamentoId, StatusDoPagamento status)
    {
        var useCase = new ConfirmarPagamentoUseCase(_logger, _pagamentoGateway, _pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(new ConfirmarPagamentoDto(pagamentoId, (StatusPagamento)status));

        return ControllerResultBuilder<PagamentoResponseDTO, Pagamento>
           .ForUseCase(useCase)
           .WithInstance(pagamentoId)
           .WithResult(useCaseResult)
           .AdaptUsing(PagamentoPresenter.ToPagamentoDTO)
           .Build();
    }

    public async Task<Result<List<PagamentoResponseDTO>>> GetPagamentoByPedidoAsync(Guid pedidoId)
    {
        var useCase = new ObterPagamentoByPedidoUseCase(_logger, _pagamentoGateway);
        var useCaseResult = await useCase.ResolveAsync(pedidoId);

        return ControllerResultBuilder<List<PagamentoResponseDTO>, List<Pagamento>>
           .ForUseCase(useCase)
           .WithInstance(pedidoId)
           .WithResult(useCaseResult)
           .AdaptUsing(PagamentoPresenter.ToListPagamentoDTO)
           .Build();
    }

    public async Task<Result<PagamentoResponseDTO>> IniciarPagamento(Guid pedidoId, MetodosDePagamento metodoDePagamento)
    {
        var useCase = new IniciarPagamentoUseCase(_logger, _pedidoGateway, _pagamentoGateway, _fornecedorPagamentoGateway);
        var useCaseResult = await useCase.ResolveAsync(new IniciarPagamentoDto(pedidoId, (MetodoDePagamento)metodoDePagamento));

        return ControllerResultBuilder<PagamentoResponseDTO, Pagamento>
           .ForUseCase(useCase)
           .WithInstance(pedidoId)
           .WithResult(useCaseResult)
           .AdaptUsing(PagamentoPresenter.ToPagamentoDTO)
           .Build();
    }
}
