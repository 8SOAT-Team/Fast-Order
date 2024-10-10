using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos;

public class ObterPagamentoByPedidoUseCase(
    ILogger logger,
    IPagamentoGateway pagamentoGateway) : UseCase<Guid, List<Pagamento>>(logger)
{
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;

    protected override async Task<List<Pagamento>?> Execute(Guid pedidoId)
    {
        var pagamentos = await _pagamentoGateway.FindPagamentoByPedidoId(pedidoId);
        return pagamentos?.Count > 0 ? pagamentos : null;
    }
}