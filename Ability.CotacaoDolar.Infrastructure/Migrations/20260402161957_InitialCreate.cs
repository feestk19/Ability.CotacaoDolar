using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ability.CotacaoDolar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CotacoesDolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxaCompra = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    TaxaVenda = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    DataHoraColeta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesDolar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotacoesDolar");
        }
    }
}
