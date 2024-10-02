namespace Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

public record NovoProdutoDTO
{
    public string Nome { get; init; } = null!;
    public string Descricao { get; init; } = null!;
    public decimal Preco { get; init; }
    public string Imagem { get; init; } = null!;
    public Guid CategoriaId { get; init; }

}