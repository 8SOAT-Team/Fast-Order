using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IPagamentoController
{
    Task<Result<PagamentoIniciadoDto>> GetPagamentoByPedidoAsync(Guid pedidoId);
    Task<Result<PagamentoIniciadoDto>> ConfirmarPagamento(Guid pagamentoId, StatusPagamento status);

    //novos
    Task<Result<PagamentoIniciadoDto>> IniciarPagamento(Guid pedidoId, MetodosDePagamento metodoDePagamento);
}
