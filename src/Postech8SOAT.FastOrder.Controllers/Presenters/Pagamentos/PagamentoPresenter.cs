using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;

public static class PagamentoPresenter
{
    internal static List<PagamentoResponseDTO> ToListPagamentoDTO(List<Pagamento> pagamentos)
    => pagamentos.Select(p => ToPagamentoDTO(p)).ToList();

    internal static PagamentoResponseDTO ToPagamentoDTO(Pagamento pagamento)
    => new(pagamento.Id,
        (MetodosDePagamento)pagamento.MetodoDePagamento,
        (StatusDoPagamento)pagamento.Status,
        pagamento.ValorTotal,
        pagamento.PagamentoExternoId!);
}
