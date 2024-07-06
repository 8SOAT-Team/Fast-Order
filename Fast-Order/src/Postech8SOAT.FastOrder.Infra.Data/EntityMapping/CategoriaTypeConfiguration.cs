using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
internal class CategoriaTypeConfiguration:IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(100);
        builder.HasData(
                       new Categoria(1, "Coca-Cola", "Coca-Cola 2L"),
                       new Categoria(2, "Hamburger X-Tudo", "Hamburger X-Tudo"),
                       new Categoria(3, "Batata Frita", "Fritas a moda da casa."));
    }
}
