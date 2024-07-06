using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
internal class PedidoProdutoTypeConfiguration:IEntityTypeConfiguration<PedidoProduto>
{
    public void Configure(EntityTypeBuilder<PedidoProduto> builder)
    {
        builder.ToTable("PedidoProdutos");
        builder.HasKey(pp => pp.Id);
        builder.Property(pp=>pp.PedidoId).IsRequired();
        builder.Property(pp=>pp.ProdutoId).IsRequired();
        builder.Property(pp => pp.Quantidade).IsRequired();
        builder.Property(pp => pp.ValorUnitario).HasPrecision(10, 2);
        builder.HasOne(pp => pp.Pedido).WithMany().HasForeignKey(pp => pp.PedidoId);
        builder.HasOne(pp => pp.Produto).WithMany().HasForeignKey(pp => pp.ProdutoId);
    }
}
