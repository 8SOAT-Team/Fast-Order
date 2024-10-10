using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos;

public class ConfirmarPagamentoUseCase(
    ILogger logger,
    IPagamentoGateway pagamentoGateway,
    IPedidoGateway pedidoGateway) : UseCase<ConfirmarPagamentoDto, Pagamento>(logger)
{
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;

    protected override async Task<Pagamento?> Execute(ConfirmarPagamentoDto dto)
    {
        var pagamento = await _pagamentoGateway.GetById(dto.PagamentoId);

        if (pagamento == null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pagamento não encontrado"));
            return null;
        }

        var pedido = await _pedidoGateway.GetByIdAsync(pagamento.PedidoId);

        if (pedido == null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pedido não encontrado"));
            return null;
        }

        pagamento.FinalizarPagamento(dto.Status == Domain.Entities.Enums.StatusPagamento.Autorizado);
        var pagamentoAtualizado = await _pagamentoGateway.UpdatePagamentoAsync(pagamento);

        if (pagamento.EstaAutorizado())
        {
            pedido.IniciarPreparo(pagamento);
            await _pedidoGateway.UpdateAsync(pedido);
        }

        return pagamentoAtualizado;
    }
}