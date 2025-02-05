using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;

internal sealed class ItemDoPedidoStubBuilder : Faker<ItemDoPedido>
{
    private static readonly Faker Faker = new();

    private ItemDoPedidoStubBuilder()
    {
        var produto = ProdutoStubBuilder.Create();
        const int qty = 1;

        CustomInstantiator(f => new ItemDoPedido(Guid.NewGuid(), produto, qty));
        Ignore(x => x.Quantidade);
        RuleFor(x => x.Quantidade, qty);
    }

    public static ItemDoPedido Create() => new ItemDoPedidoStubBuilder().Generate();

    public static List<ItemDoPedido> CreateMany(Func<Faker, int> qty) => CreateMany(qty(Faker));

    public static List<ItemDoPedido> CreateMany(int qty) => new ItemDoPedidoStubBuilder().Generate(qty);
}