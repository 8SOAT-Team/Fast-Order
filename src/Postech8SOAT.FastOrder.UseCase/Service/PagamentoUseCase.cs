using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;

public class PagamentoUseCase : IPagamentoUseCase
{
    private readonly IPagamentoGateway _pagamentoGateway;
    private static readonly StatusPagamento[] _statusPagamentosPodemConfirmar = [StatusPagamento.Autorizado, StatusPagamento.Rejeitado, StatusPagamento.Cancelado];

    public PagamentoUseCase(IPagamentoGateway pagamentoGateway)
    {
        _pagamentoGateway = pagamentoGateway;
    }

    public Task<Pagamento?> GetPagamentoAsync(Guid pagamentoId) => _pagamentoGateway.GetPagamentoAsync(pagamentoId);

    public async Task<Pagamento> CreatePagamentoAsync(Pedido pedido, MetodoDePagamento metodoDePagamento)
    {
        return await _pagamentoGateway.CreatePagamentoAsync(pedido, metodoDePagamento);
    }

    public async Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento)
    {
        await _pagamentoGateway.UpdatePagamentoAsync(pagamento);
        return pagamento;
    }

    public Task<List<Pagamento>> ListPagamentos() => _pagamentoGateway.ListPagamentos();

    public Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId) => _pagamentoGateway.FindPagamentoByPedidoId(pedidoId);

    public Task<Pagamento?> GetPagamentoByPedidoAsync(Guid pedidoId) => _pagamentoGateway.GetPagamentoByPedidoAsync(pedidoId);

    public async Task ConfirmarPagamento(Guid pagamentoId, StatusPagamento status)
    {
       await _pagamentoGateway.ConfirmarPagamento(pagamentoId,status);
    }
}