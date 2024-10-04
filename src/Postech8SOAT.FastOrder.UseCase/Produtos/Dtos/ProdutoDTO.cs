namespace Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

public record ProdutoDTO
{    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Descricao { get; init; } = null!;
    public decimal Preco { get; init; }
    public string Imagem { get; init; } = null!;
    public ProdutoCategoriaDTO Categoria { get; set; } = null!;
}