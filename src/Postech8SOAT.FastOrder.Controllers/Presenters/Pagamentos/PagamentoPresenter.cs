using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;

public static class PagamentoPresenter
{
    internal static PagamentoIniciadoDto PagamentoIniciado(Pagamento pagamento)
    => new(pagamento.Id,
        (MetodosDePagamento)pagamento.MetodoDePagamento,
        (StatusDoPagamento)pagamento.Status,
        pagamento.ValorTotal,
        pagamento.PagamentoExternoId!);
}
