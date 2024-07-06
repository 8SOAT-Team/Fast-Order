using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postech8SOAT.FastOrder.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos (Id,Nome, Descricao, CategoriaId, Preco, Imagem) " +
                "VALUES " +
                "('0e05db30-b5ec-4e26-b79e-a43b64743ab5','Coca-Cola', 'Refrigerante Coca-Cola 2L', '6224B6C0-26E9-42FA-8B04-DC0E9FD6B971', 7.50, 'coca-cola.jpg')");
            migrationBuilder.Sql("INSERT INTO Produtos (Id,Nome, Descricao, CategoriaId, Preco, Imagem) " +
                "VALUES " +
                "('f0fdbefb-08b2-4ad7-bdfc-8c3f0e070d8e','Hamburger X-Tudo', 'Hamburger X-Tudo', '0194D8C4-2D04-4172-A63A-4D381EADF729', 15.00, 'hamburger.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
