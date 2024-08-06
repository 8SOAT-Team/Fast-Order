using Postech8SOAT.FastOrder.Domain.Exceptions;
using System.Text.Encodings.Web;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Produto : Entity, IAggregateRoot
{
    public string Nome { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public virtual Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
    public string Imagem { get; private set; } = null!;

    protected Produto() { }

    public Produto(string nome, string descricao, decimal preco, string imagem, Guid categoriaId)
        : this(Guid.NewGuid(), nome, descricao, preco, imagem, categoriaId) { }

    public Produto(Guid id, string nome, string descricao, decimal preco, string imagem, Guid categoriaId)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        ValidationDomain(nome, descricao, preco, imagem, categoriaId);

        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Imagem = imagem;
        CategoriaId = categoriaId;
    }

    private static void ValidationDomain(string nome, string descricao, decimal preco, string imagem, Guid categoriaId)
    {
        ValidateDomainNome(nome);
        ValidateDomainDescricao(descricao);
        ValidateDomainImagem(imagem);
        ValidateDomainCategoria(categoriaId);
        ValidateDomainPreco(preco);
    }

    private static void ValidateDomainNome(string nome)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");
    }

    private static void ValidateDomainDescricao(string descricao)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição é obrigatória");

        DomainExceptionValidation.When(descricao!.Length < 3, "Descrição deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");
    }

    private static void ValidateDomainImagem(string imagem)
    {
        DomainExceptionValidation.When(imagem!.Length < 3, "Endereço da imagem deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(imagem.Length > 300, "Endereço da imagem deve ter no máximo 300 caracteres");
        DomainExceptionValidation.When(Uri.IsWellFormedUriString(imagem, UriKind.Absolute) is false, "URL da imagem inválida.");
    }

    private static void ValidateDomainCategoria(Guid categoriaId)
    {
        DomainExceptionValidation.When(categoriaId == Guid.Empty, "Id inválido");
    }

    private static void ValidateDomainPreco(decimal preco)
    {
        DomainExceptionValidation.When(preco < 0, "Preço inválido");
    }

    public void RenameTo(string nome)
    {
        ValidateDomainNome(nome);
        Nome = nome;
    }

    public void DescribeAs(string descricao)
    {
        ValidateDomainDescricao(descricao);
        Descricao = descricao;
    }

    public void SetPreco(decimal preco)
    {
        ValidateDomainPreco(preco);
        Preco = preco;
    }

    public void SubstituteImagem(string imagem)
    {
        ValidateDomainImagem(imagem);
        Imagem = imagem;
    }

    public void ChangeToCategory(Categoria categoria)
    {
        ValidateDomainCategoria(categoria.Id);
        CategoriaId = categoria.Id;
        Categoria = categoria;
    }
}
