using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Migrations
{
    /// <inheritdoc />
    public partial class AddFornecedorInProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fornecedor",
                table: "Produto",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fornecedor",
                table: "Produto");
        }
    }
}
