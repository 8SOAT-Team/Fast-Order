using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Cliente:Entity
{
    protected Cliente()
    {
        
    }
    public string? Cpf { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }

    public Cliente(string? cpf, string? nome, string? email)
    {
        ValidationDomain(cpf, nome, email);
    }

    public Cliente(int id, string? cpf, string? nome, string? email)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id;
        ValidationDomain(cpf, nome, email);
    }

    public void Update(string? cpf, string? nome, string? email)
    {
        ValidationDomain(cpf, nome, email);
    }

    private void ValidationDomain(string? cpf, string? nome, string? email)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "Cpf é obrigatório");

        DomainExceptionValidation.When(cpf!.Length < 11, "Cpf deve ter no mínimo 11 caracteres");

        DomainExceptionValidation.When(cpf.Length > 11, "Cpf deve ter no máximo 11 caracteres");

        DomainExceptionValidation.When(cpf.Length < 11 || cpf.Length > 11, "Cpf deve ter 11 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");

        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(nome.Length < 3 || nome.Length > 100, "Nome deve ter entre 3 e 100 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email é obrigatório");

        DomainExceptionValidation.When(email!.Length < 3, "Email deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(email.Length > 100, "Email deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(email.Length < 3 || email.Length > 100, "Email deve ter entre 3 e 100 caracteres");

        this.Cpf = cpf;
        this.Nome = nome;
        this.Email = email;
    }
}
