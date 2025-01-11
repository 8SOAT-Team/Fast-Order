using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class ProdutoBuilder : Faker<Produto>
{
    public ProdutoBuilder()
    {
        CustomInstantiator(f => new Produto(nome:f.Commerce.ProductName(),descricao:f.Commerce.ProductDescription().Substring(0,30), preco:decimal.Parse(f.Commerce.Price(1,1000)),imagem: f.Image.LoremFlickrUrl()!, RetornaIdCategoriaUtil.RetornaIdCategoria()));
    }
    public Produto Build() => Generate();

}
