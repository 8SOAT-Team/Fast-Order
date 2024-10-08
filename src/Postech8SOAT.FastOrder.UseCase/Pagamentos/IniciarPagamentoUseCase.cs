using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos;


public class IniciarPagamentoUseCase(
    ILogger logger,
    IPedidoGateway pedidoGateway,
    IPagamentoGateway pagamentoGateway) : UseCase<IniciarPagamentoDto, Pagamento>(logger)
{
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;

    protected override async Task<Pagamento?> Execute(IniciarPagamentoDto command)
    {
        var pedido = await _pedidoGateway.GetByIdAsync(command.PedidoId);

        if (pedido is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pedido não encontrado"));
            return null;
        }

        if(pedido.Pagamento is not null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pagamento já iniciado"));
            return null;
        }

        pedido.IniciarPagamento(command.MetodoDePagamento);

        await _pedidoGateway.AtualizarPedidoPagamentoIniciadoAsync(pedido);

        return pedido.Pagamento;
    }
}
