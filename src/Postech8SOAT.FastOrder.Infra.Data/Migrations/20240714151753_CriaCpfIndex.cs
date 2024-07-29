using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postech8SOAT.FastOrder.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriaCpfIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Cpf",
                table: "Clientes",
                column: "Cpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_Cpf",
                table: "Clientes");
        }
    }
}
