using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class ProdutoBuilder : Faker<Produto>
{
    public ProdutoBuilder()
    {
        CustomInstantiator(f => new Produto(f.Commerce.ProductName(),f.Commerce.ProductDescription(), f.Random.Decimal(1, 1000),f.Random.String(),f.Random.Guid()));
    }
    public Produto Build() => Generate();

}
