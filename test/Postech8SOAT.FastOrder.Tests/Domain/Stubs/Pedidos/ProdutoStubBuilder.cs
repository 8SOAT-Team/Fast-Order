using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;

internal sealed class ProdutoStubBuilder : Faker<Produto>
{
    private ProdutoStubBuilder()
    {
        var categoria = CategoriaStubBuilder.Create();

        CustomInstantiator(f => new Produto(f.Commerce.ProductName(), f.Commerce.ProductName(), f.Random.Int(1, 1000),
            "http://fast-order-imagens.biz/produto-imagem", categoria.Id));

        RuleFor(x => x.Categoria, categoria);
        RuleFor(x => x.Imagem, "http://fast-order-imagens.biz/produto-imagem");
    }

    public static Produto Create() => new ProdutoStubBuilder().Generate();
    public static List<Produto> CreateMany(int qty) => new ProdutoStubBuilder().Generate(qty);
}