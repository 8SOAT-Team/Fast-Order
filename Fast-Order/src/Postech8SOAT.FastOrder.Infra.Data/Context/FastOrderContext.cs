using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.Context;
public class FastOrderContext:DbContext
{
    public FastOrderContext()
    {
        
    }
    public FastOrderContext(DbContextOptions<FastOrderContext> options):base(options)
    {
        
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FastOrderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        optionsBuilder         
            .UseSqlServer(connectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FastOrderContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
