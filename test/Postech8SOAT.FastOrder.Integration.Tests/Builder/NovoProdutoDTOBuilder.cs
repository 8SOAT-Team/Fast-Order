using Bogus;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class NovoProdutoDTOBuilder : Faker<NovoProdutoDTO>
{
    public NovoProdutoDTOBuilder()
    {
        CustomInstantiator(f => new NovoProdutoDTO()
        {
            Nome = f.Commerce.ProductName().Substring(0,10),
            Descricao = f.Commerce.ProductDescription().Substring(0,10),
            Preco = f.Random.Decimal(5, 1000),
            Imagem = f.Image.LoremFlickrUrl(),
            CategoriaId = RetornaIdCategoriaUtil.RetornaIdCategoria()
        });
    }
    public NovoProdutoDTO Build() => Generate();


}
