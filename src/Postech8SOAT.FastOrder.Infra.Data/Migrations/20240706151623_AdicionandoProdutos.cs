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
                "('0e05db30-b5ec-4e26-b79e-a43b64743ab5','Coca-Cola', 'Refrigerante Coca-Cola 2L', '07c470aa-606f-4792-849a-02433c121117', 7.50, 'coca-cola.jpg')");
            migrationBuilder.Sql("INSERT INTO Produtos (Id,Nome, Descricao, CategoriaId, Preco, Imagem) " +
                "VALUES " +
                "('f0fdbefb-08b2-4ad7-bdfc-8c3f0e070d8e','Hamburger X-Tudo', 'Hamburger X-Tudo', '6224b6c0-26e9-42fa-8b04-dc0e9fd6b971', 15.00, 'hamburger.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
