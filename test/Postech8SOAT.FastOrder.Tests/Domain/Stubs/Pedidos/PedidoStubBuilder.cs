using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;

internal sealed class PedidoStubBuilder : Faker<Pedido>
{
    public PedidoStubBuilder()
    {
        var cliente = ClienteStubBuilder.Create();
        var itensDoPedido = ItemDoPedidoStubBuilder.CreateMany(f => f.Random.Int(1, 5));
        CustomInstantiator(f => new Pedido(cliente.Id, itensDoPedido));
        RuleFor(x => x.Cliente, cliente);
    }

    public PedidoStubBuilder WithStatus(StatusPedido status)
    {
        RuleFor(x => x.StatusPedido, status);
        return this;
    }

    public PedidoStubBuilder WithPagamento(StatusPagamento status)
    {
        RuleFor(x => x.Pagamento, PagamentoStubBuilder.NewBuilder().WithStatus(status).Generate());
        return this;
    }

    public static PedidoStubBuilder NewBuilder() => new();
    public static Pedido Create() => new PedidoStubBuilder().Generate();
    public static List<Pedido> CreateMany(int qty) => new PedidoStubBuilder().Generate(qty);
}