using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Pagamentos;

public class ConfirmarStatusPagamentoUseCase(ILogger logger, 
    IFornecedorPagamentoGateway fornecedorPagamentoGateway,
    IPagamentoGateway pagamentoGateway) : UseCase<string, Pagamento>(logger)
{
    private readonly IFornecedorPagamentoGateway _fornecedorPagamentoGateway = fornecedorPagamentoGateway;
    private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;

    protected override async Task<Pagamento?> Execute(string pagamentoExternoId)
    {
        var pagamentoExterno = await _fornecedorPagamentoGateway.ObterPagamento(pagamentoExternoId);

        if (pagamentoExterno is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pagamento Externo não encontrado"));
            return null;
        }

        var pagamento = await _pagamentoGateway.GetByIdAsync(pagamentoExterno.PagamentoId);

        if (pagamento is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pagamento não encontrado"));
            return null;
        }

        if (pagamentoExterno.StatusPagamento == StatusPagamento.Pendente)
        {
            return pagamento;
        }

        pagamento.FinalizarPagamento(pagamentoExterno.StatusPagamento == StatusPagamento.Autorizado);
        var pagamentoAtualizado = await _pagamentoGateway.UpdatePagamentoAsync(pagamento);

        return pagamentoAtualizado;
    }
}
