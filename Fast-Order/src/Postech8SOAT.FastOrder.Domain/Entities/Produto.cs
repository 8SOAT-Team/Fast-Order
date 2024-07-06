using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Produto:Entity
{
    protected Produto()
    {
        this.Id = Guid.NewGuid();
    }
    public string? Nome { get;  set; }
    public string? Descricao { get;  set; }
    public decimal Preco { get;  set; }
    public virtual Guid CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }
    public string? Imagem { get; set; }

    public Produto(string? nome, string? descricao, decimal preco,string imagem,Guid categoriaId)
    {
        ValidationDomain(nome, descricao, preco,imagem, categoriaId);
    }

    public Produto(Guid id,string? nome, string? descricao, decimal preco,string imagem, Guid categoriaId)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(id == null, "Id inválido");
        Id = id;
        ValidationDomain(nome, descricao, preco,imagem, categoriaId);
    }
    private void ValidationDomain(string? nome, string? descricao, decimal preco,string image, Guid categoriaId)
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
        DomainExceptionValidation.When(categoriaId == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(categoriaId == null, "Id inválido");
        

        this.Nome = nome;
        this.Descricao = descricao;
        this.Preco = preco;
        this.Imagem = image;
        this.CategoriaId = categoriaId;
    }

    public void Update(string? nome, string? descricao, decimal preco,string imagem, Guid categoriaId)
    {
        ValidationDomain(nome, descricao, preco, imagem, categoriaId);
    }
}
