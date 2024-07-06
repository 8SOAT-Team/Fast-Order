using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Pedido:Entity
{
    protected Pedido()
    {
        this.Id = Guid.NewGuid();
    }
    public DateTime DataPedido { get; set; }
    public StatusPedido? StatusPedido { get; set; }
    public virtual Guid ClienteId { get; set; }
    public virtual Cliente? Cliente { get; set; }
    public virtual ICollection<ItemDoPedido>? ItensDoPedido { get; set; }
    public Decimal ValorTotal { get; private set; }

    public event Action<decimal> ValorTotalCalculado = delegate { };

    public void AdicionarProduto(ItemDoPedido item)
    {
        this.ItensDoPedido.Add(item);
        ValorTotalCalculado.Invoke(CalcularValorTotal());
    }

    public void RemoverProduto(ItemDoPedido item)
    {
        this.ItensDoPedido.Remove(item);
        ValorTotalCalculado.Invoke(CalcularValorTotal());
    }

    public decimal CalcularValorTotal()
    {
        decimal total = 0;
        foreach (var item in this.ItensDoPedido)
        {
            total += item.Produto.Preco;
        }
        return total;
    }

    public Pedido(string? dataPedido, StatusPedido? statusPedido, Guid clienteId, List<ItemDoPedido>? itens, decimal valortotal)
    {
        ValidationDomain(dataPedido, statusPedido, clienteId, itens, valortotal);
    }

    public Pedido(Guid id, string? dataPedido, StatusPedido? statusPedido, Guid clienteId, List<ItemDoPedido>? itens, decimal valortotal)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(id == null, "Id inválido");
        Id = id;
        ValidationDomain(dataPedido, statusPedido, clienteId, itens, valortotal);
    }

    public void Update(string? dataPedido, StatusPedido? statusPedido, Guid clienteId, List<ItemDoPedido>? itens, decimal valortotal)
    {
        ValidationDomain(dataPedido, statusPedido, clienteId, itens, valortotal);
    }

    private void ValidationDomain(string? dataPedido, StatusPedido? statusPedido, Guid clienteId, List<ItemDoPedido>? itens,decimal valortotal)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(dataPedido), "Data do pedido é obrigatória");

        DomainExceptionValidation.When(statusPedido == null, "Status do pedido é obrigatório");

        DomainExceptionValidation.When(clienteId == null, "Cliente é obrigatório");

        DomainExceptionValidation.When(itens == null, "Itens são obrigatórios");

        DomainExceptionValidation.When(valortotal < 0, "Valor total inválido");

        DomainExceptionValidation.When(valortotal == 0, "Valor total inválido");

        this.DataPedido = Convert.ToDateTime(dataPedido);
        this.StatusPedido = statusPedido;
        this.ClienteId = clienteId;
        this.ItensDoPedido = itens;
        this.ValorTotal = valortotal;
    }
}
