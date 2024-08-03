using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
internal class PedidoTypeConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.DataPedido).IsRequired();
        builder.Property(p => p.ValorTotal).HasPrecision(18, 2);
        builder.Property(p => p.ClienteId).IsRequired(false);
        builder.Property(p => p.StatusPedido)
              .HasConversion(fromObj => Convert.ToInt32(fromObj), fromDb => (StatusPedido)fromDb);
        builder.HasOne(p => p.Cliente).WithMany().HasForeignKey(p => p.ClienteId);
    }
}

