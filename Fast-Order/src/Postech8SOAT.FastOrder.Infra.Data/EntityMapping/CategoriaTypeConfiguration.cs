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
                       new Categoria(Guid.Parse("6224b6c0-26e9-42fa-8b04-dc0e9fd6b971"), "Coca-Cola", "Coca-Cola 2L"),
                       new Categoria(Guid.Parse("0194d8c4-2d04-4172-a63a-4d381eadf729"), "Hamburger X-Tudo", "Hamburger X-Tudo"),
                       new Categoria(Guid.Parse("07c470aa-606f-4792-849a-02433c121117"), "Batata Frita", "Fritas a moda da casa."));
    }
}
