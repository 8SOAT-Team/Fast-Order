using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
internal class CategoriaTypeConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(100);
        builder.HasData(
                       new Categoria(Guid.Parse("6224b6c0-26e9-42fa-8b04-dc0e9fd6b971"), "Lanche", "Lanches"),
                       new Categoria(Guid.Parse("0194d8c4-2d04-4172-a63a-4d381eadf729"), "Acompanhamento", "Acompanhamentos"),
                       new Categoria(Guid.Parse("07c470aa-606f-4792-849a-02433c121117"), "Bebida", "Bebidas"),
                       new Categoria(Guid.Parse("B553A212-9930-4E5A-A780-138A0A4A0B78"), "Sobremesa", "Sobremesas"));
    }
}
