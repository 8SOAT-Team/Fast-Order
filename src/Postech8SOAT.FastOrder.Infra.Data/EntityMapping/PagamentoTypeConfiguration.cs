using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;

internal class PagamentoTypeConfiguration : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamentos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.PedidoId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.MetodoDePagamento).IsRequired();
        builder.Property(p => p.ValorTotal).HasPrecision(18, 2);
        builder.Property(p => p.PagamentoExternoId).IsRequired(false);

        builder.HasOne(p => p.Pedido).WithOne(p => p.Pagamento).HasForeignKey<Pagamento>(p => p.PedidoId);
    }
}
