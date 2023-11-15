using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aplicacaoLoja.Migrations
{
    /// <inheritdoc />
    public partial class Dadoss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgrVendas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clienteId = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgrVendas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaPivot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cliente = table.Column<int>(type: "int", nullable: false),
                    mes1 = table.Column<float>(type: "real", nullable: false),
                    mes2 = table.Column<float>(type: "real", nullable: false),
                    mes3 = table.Column<float>(type: "real", nullable: false),
                    mes4 = table.Column<float>(type: "real", nullable: false),
                    mes5 = table.Column<float>(type: "real", nullable: false),
                    mes6 = table.Column<float>(type: "real", nullable: false),
                    mes7 = table.Column<float>(type: "real", nullable: false),
                    mes8 = table.Column<float>(type: "real", nullable: false),
                    mes9 = table.Column<float>(type: "real", nullable: false),
                    mes10 = table.Column<float>(type: "real", nullable: false),
                    mes11 = table.Column<float>(type: "real", nullable: false),
                    mes12 = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaPivot", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgrVendas");

            migrationBuilder.DropTable(
                name: "ConsultaPivot");
        }
    }
}
