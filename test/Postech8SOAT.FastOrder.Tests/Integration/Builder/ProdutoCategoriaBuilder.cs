using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
internal class ProdutoCategoriaBuilder : Faker<List<ProdutoCategoriaDTO>>
{
    public ProdutoCategoriaBuilder()
    {
        CustomInstantiator(f => new List<ProdutoCategoriaDTO>
        {
            new ProdutoCategoriaDTO(){Id=f.Random.Guid(),Descricao=f.Random.String(),Nome=f.Random.String()},
            new ProdutoCategoriaDTO(){Id=f.Random.Guid(),Descricao=f.Random.String(),Nome=f.Random.String()},
            new ProdutoCategoriaDTO(){Id=f.Random.Guid(),Descricao=f.Random.String(),Nome=f.Random.String()},
            new ProdutoCategoriaDTO(){Id=f.Random.Guid(),Descricao=f.Random.String(),Nome=f.Random.String()},
        });
    }
    public List<ProdutoCategoriaDTO> Build() => Generate();

}
