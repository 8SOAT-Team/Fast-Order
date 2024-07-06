using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
internal class ProdutoTypeConfiguration:IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Preco).HasPrecision(10, 2);
        builder.Property(p => p.Imagem).IsRequired().HasMaxLength(100);
        builder.Property(p => p.StatusPedido)
              .HasConversion(
                fromObj => fromObj.ToString(),
                fromDb => (StatusPedido)Enum.Parse(typeof(StatusPedido), fromDb)
            );
        builder.HasOne(p => p.Categoria).WithMany().HasForeignKey(p => p.CategoriaId);
    }
}
