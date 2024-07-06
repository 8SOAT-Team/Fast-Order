using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Pedido:Entity
{
    public DateTime DataPedido { get; set; }
    public StatusPedido? StatusPedido { get; set; }
    public int? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public List<Produto>? Produtos { get; set; }
    public Decimal ValorTotal { get; }

    public event Action<decimal> ValorTotalCalculado = delegate { };

    public void AdicionarProduto(Produto produto)
    {
        this.Produtos.Add(produto);
        ValorTotalCalculado.Invoke(CalcularValorTotal());
    }

    public void RemoverProduto(Produto produto)
    {
        this.Produtos.Remove(produto);
        ValorTotalCalculado.Invoke(CalcularValorTotal());
    }

    public decimal CalcularValorTotal()
    {
        decimal total = 0;
        foreach (var produto in this.Produtos)
        {
            total += produto.Preco;
        }
        return total;
    }

    public Pedido(string? dataPedido, StatusPedido? statusPedido, int? clienteId, List<Produto>? produtos,decimal valortotal)
    {
        ValidationDomain(dataPedido, statusPedido, clienteId, produtos, valortotal);
    }

    public Pedido(int id, string? dataPedido, StatusPedido? statusPedido, int? clienteId, List<Produto>? produtos, decimal valortotal)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id;
        ValidationDomain(dataPedido, statusPedido, clienteId, produtos,valortotal);
    }

    public void Update(string? dataPedido, StatusPedido? statusPedido, int? clienteId, List<Produto>? produtos, decimal valortotal)
    {
        ValidationDomain(dataPedido, statusPedido, clienteId, produtos,valortotal);
    }

    private void ValidationDomain(string? dataPedido, StatusPedido? statusPedido, int? clienteId, List<Produto>? produtos,decimal valortotal)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(dataPedido), "Data do pedido é obrigatória");

        DomainExceptionValidation.When(statusPedido == null, "Status do pedido é obrigatório");

        DomainExceptionValidation.When(clienteId == null, "Cliente é obrigatório");

        DomainExceptionValidation.When(produtos == null, "Produtos são obrigatórios");

        DomainExceptionValidation.When(valortotal < 0, "Valor total inválido");

        DomainExceptionValidation.When(valortotal == 0, "Valor total inválido");

        this.DataPedido = Convert.ToDateTime(dataPedido);
        this.StatusPedido = statusPedido;
        this.ClienteId = clienteId;
        this.Produtos = produtos;
        this.ValorTotal = valortotal;
    }
}
