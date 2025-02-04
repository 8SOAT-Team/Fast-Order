using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
internal class CategoriasBuilder:Faker<List<Categoria>>
{
    public CategoriasBuilder()
    {
        CustomInstantiator(f => new List<Categoria>
        {
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String()),
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String()),
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String()),
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String()),
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String()),
            new Categoria(f.Random.Guid(),f.Random.String(),f.Random.String())

        });
    }
    public List<Categoria> Build() => Generate();
}
