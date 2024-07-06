namespace Postech8SOAT.FastOrder.Domain.Entities;
public class PedidoProduto:Entity
{
    protected PedidoProduto()
    {
        
    }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }  
    public decimal ValorTotal { get; set; }
}
