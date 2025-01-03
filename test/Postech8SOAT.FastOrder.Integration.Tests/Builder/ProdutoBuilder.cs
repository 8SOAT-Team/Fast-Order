using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class ProdutoBuilder : Faker<Produto>
{
    public ProdutoBuilder()
    {
        CustomInstantiator(f => new Produto(f.Commerce.ProductName(),f.Commerce.ProductDescription(), decimal.Parse(f.Commerce.Price(1,1000)),f.Commerce.Random.ToString()!, RetornaIdCategoriaUtil.RetornaIdCategoria()));
    }
    public Produto Build() => Generate();

}
