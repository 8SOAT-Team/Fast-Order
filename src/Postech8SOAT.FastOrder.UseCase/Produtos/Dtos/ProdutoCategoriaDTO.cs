namespace Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

public class ProdutoCategoriaDTO
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Descricao { get; init; } = null!;
}
