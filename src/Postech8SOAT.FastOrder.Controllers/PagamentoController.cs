using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class PagamentoController:IPagamentoController
{
   private readonly IPagamentoUseCase _pagamentoUseCase;
     public PagamentoController(IPagamentoUseCase pagamentoUseCase)
     {
        _pagamentoUseCase = pagamentoUseCase;
     }

    public Task<Pagamento?> GetPagamentoAsync(Guid pagamentoId)
    {
      return _pagamentoUseCase.GetPagamentoAsync(pagamentoId);
    }

    public Task<Pagamento?> GetPagamentoByPedidoAsync(Guid pedidoId)
    {
      return _pagamentoUseCase.GetPagamentoByPedidoAsync(pedidoId);
    }

    public Task<Pagamento> CreatePagamentoAsync(Pedido pedido, MetodoDePagamento metodoDePagamento)
    {
      return _pagamentoUseCase.CreatePagamentoAsync(pedido, metodoDePagamento);
    }

    public Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento)
    {
      return _pagamentoUseCase.UpdatePagamentoAsync(pagamento);
    }

    public Task<List<Pagamento>> ListPagamentos()
    {
      return _pagamentoUseCase.ListPagamentos();
    }
   
    public Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId)
    {
      return _pagamentoUseCase.FindPagamentoByPedidoId(pedidoId);
    }

    public Task ConfirmarPagamento(Guid pagamentoId, StatusPagamento status)
    {
      return _pagamentoUseCase.ConfirmarPagamento(pagamentoId, status);
    }

      
}
