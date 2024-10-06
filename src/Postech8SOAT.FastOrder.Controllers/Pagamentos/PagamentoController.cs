using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Pagamentos;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Pagamentos;

public class PagamentoController(ILogger logger, IPedidoGateway pedidoGateway, IPagamentoGateway pagamentoGateway) : IPagamentoController
{
    private readonly ILogger _logger = logger;
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;

    public Task<Result<PagamentoIniciadoDto>> ConfirmarPagamento(Guid pagamentoId, StatusPagamento status)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagamentoIniciadoDto>> GetPagamentoByPedidoAsync(Guid pedidoId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagamentoIniciadoDto>> IniciarPagamento(Guid pedidoId, MetodosDePagamento metodoDePagamento)
    {
        var useCase = new IniciarPagamentoUseCase(_logger, _pedidoGateway, _pagamentoGateway);
        var useCaseResult = await useCase.ResolveAsync(new IniciarPagamentoDto(pedidoId, (MetodoDePagamento)metodoDePagamento));

        return ControllerResultBuilder<PagamentoIniciadoDto, Pagamento>
           .ForUseCase(useCase)
           .WithInstance(pedidoId)
           .WithResult(useCaseResult)
           .AdaptUsing(PagamentoPresenter.PagamentoIniciado)
           .Build();
    }
}
