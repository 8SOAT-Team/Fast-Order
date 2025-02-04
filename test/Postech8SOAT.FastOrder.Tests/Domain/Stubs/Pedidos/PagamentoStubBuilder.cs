using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;

internal sealed class PagamentoStubBuilder : Faker<Pagamento>
{
    private PagamentoStubBuilder()
    { 
        var pedido = PedidoStubBuilder.Create();
        CustomInstantiator(f => new Pagamento(Guid.NewGuid(), pedido.Id, pedido,
            MetodoDePagamento.Pix, f.Random.Decimal(1, 1000), f.Random.Guid().ToString()));
    }

    public PagamentoStubBuilder WithStatus(StatusPagamento status)
    {
        RuleFor(x => x.Status, status);
        return this;
    }

    public static PagamentoStubBuilder NewBuilder() => new();
    public static Pagamento Create() => new PagamentoStubBuilder().Generate();
    public static List<Pagamento> CreateMany(int qty) => new PagamentoStubBuilder().Generate(qty);
    public static Pagamento Autorizado() => NewBuilder().WithStatus(StatusPagamento.Autorizado).Generate();
    public static Pagamento Rejeitado() => NewBuilder().WithStatus(StatusPagamento.Rejeitado).Generate();
    public static Pagamento Cancelado() => NewBuilder().WithStatus(StatusPagamento.Cancelado).Generate();
}