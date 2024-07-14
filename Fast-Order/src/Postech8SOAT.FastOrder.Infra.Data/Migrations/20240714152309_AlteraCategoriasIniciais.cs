using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postech8SOAT.FastOrder.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteraCategoriasIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("0194d8c4-2d04-4172-a63a-4d381eadf729"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Acompanhamentos", "Acompanhamento" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("07c470aa-606f-4792-849a-02433c121117"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Bebidas", "Bebida" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("6224b6c0-26e9-42fa-8b04-dc0e9fd6b971"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Lanches", "Lanche" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[] { new Guid("b553a212-9930-4e5a-a780-138a0a4a0b78"), "Sobremesas", "Sobremesa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("b553a212-9930-4e5a-a780-138a0a4a0b78"));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("0194d8c4-2d04-4172-a63a-4d381eadf729"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Hamburger X-Tudo", "Hamburger X-Tudo" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("07c470aa-606f-4792-849a-02433c121117"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Fritas a moda da casa.", "Batata Frita" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("6224b6c0-26e9-42fa-8b04-dc0e9fd6b971"),
                columns: new[] { "Descricao", "Nome" },
                values: new object[] { "Coca-Cola 2L", "Coca-Cola" });
        }
    }
}
