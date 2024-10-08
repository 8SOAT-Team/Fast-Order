using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways.Interfaces;

namespace Postech8SOAT.FastOrder.Gateways;
public class PagamentoGateway : IPagamentoGateway
{

    private static readonly StatusPagamento[] _statusPagamentosPodemConfirmar = [StatusPagamento.Autorizado, StatusPagamento.Rejeitado, StatusPagamento.Cancelado];

    public PagamentoGateway()
    {
    }

    public Task ConfirmarPagamento(Guid pagamentoId, StatusPagamento status)
    {
        throw new NotImplementedException();
    }

    public Task<Pagamento> CreatePagamentoAsync(Pedido pedido, MetodoDePagamento metodoDePagamento)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId)
    {
        throw new NotImplementedException();
    }

    public Task<Pagamento?> GetPagamentoAsync(Guid pagamentoId)
    {
        throw new NotImplementedException();
    }

    public Task<Pagamento?> GetPagamentoByPedidoAsync(Guid pedidoId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pagamento>> ListPagamentos()
    {
        throw new NotImplementedException();
    }

    public Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento)
    {
        throw new NotImplementedException();
    }
}
