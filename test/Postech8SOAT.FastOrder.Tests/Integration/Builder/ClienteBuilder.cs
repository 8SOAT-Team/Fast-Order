using Bogus;
using Bogus.Extensions.Brazil;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder; 
public class ClienteBuilder : Faker<Cliente>
{
    public ClienteBuilder()
    {
        CustomInstantiator(f => new Cliente(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }
    public Cliente Build() => Generate();
}
