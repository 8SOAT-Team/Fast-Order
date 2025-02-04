using Bogus;
using Bogus.Extensions.Brazil;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Tests.Domain.Stubs.Pedidos;

internal sealed class ClienteStubBuilder : Faker<Cliente>
{
    private ClienteStubBuilder()
    {
        CustomInstantiator(f => new Cliente(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }

    public static Cliente Create() => new ClienteStubBuilder().Generate();
}