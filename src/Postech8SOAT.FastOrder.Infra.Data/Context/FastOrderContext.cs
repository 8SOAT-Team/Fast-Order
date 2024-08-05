﻿using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.Context;
public class FastOrderContext : DbContext
{
    public FastOrderContext()
    {

    }
    public FastOrderContext(DbContextOptions<FastOrderContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemDoPedido> ItensDoPedido { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FastOrderContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
