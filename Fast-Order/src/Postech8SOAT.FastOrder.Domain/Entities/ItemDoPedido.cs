namespace Postech8SOAT.FastOrder.Domain.Entities;
public class ItemDoPedido:Entity
{
    public ItemDoPedido()
    {
        this.Id = Guid.NewGuid();
    }
    public Guid PedidoId { get; set; }
    public virtual Pedido Pedido { get; set; }
    public Guid ProdutoId { get; set; }
    public virtual Produto Produto { get; set; }
}
