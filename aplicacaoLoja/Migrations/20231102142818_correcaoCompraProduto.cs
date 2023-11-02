using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aplicacaoLoja.Migrations
{
    /// <inheritdoc />
    public partial class correcaoCompraProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qtdeEstoque",
                table: "CompraProdutos",
                newName: "qtde");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qtde",
                table: "CompraProdutos",
                newName: "qtdeEstoque");
        }
    }
}
