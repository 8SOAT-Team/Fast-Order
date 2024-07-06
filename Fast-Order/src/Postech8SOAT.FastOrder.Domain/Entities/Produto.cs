using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Produto:Entity
{
    public string? Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public string? Imagem { get; private set; }

    public Produto(string? nome, string? descricao, decimal preco,string imagem)
    {
        ValidationDomain(nome, descricao, preco,imagem);
    }

    public Produto(int id,string? nome, string? descricao, decimal preco,string imagem)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id;
       ValidationDomain(nome, descricao, preco,imagem);
    }
    private void ValidationDomain(string? nome, string? descricao, decimal preco,string image)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");

        DomainExceptionValidation.When(image!.Length < 3, "Endereço da imagem deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(image.Length > 150, "Endereço da imagem deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(nome.Length < 3 || nome.Length > 100, "Nome deve ter entre 3 e 100 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição é obrigatória");

        DomainExceptionValidation.When(descricao!.Length < 3, "Descrição deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(descricao.Length < 3 || descricao.Length > 100, "Descrição deve ter entre 3 e 100 caracteres");

        DomainExceptionValidation.When(preco < 0, "Preço inválido");

        this.Nome = nome;
        this.Descricao = descricao;
        this.Preco = preco;
        this.Imagem = image;
    }

    public void Update(string? nome, string? descricao, decimal preco,string imagem)
    {
        ValidationDomain(nome, descricao, preco, imagem);
    }
}
