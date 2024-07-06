namespace Postech8SOAT.FastOrder.Domain.Entities;
public class PedidoProduto:Entity
{
    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
}
