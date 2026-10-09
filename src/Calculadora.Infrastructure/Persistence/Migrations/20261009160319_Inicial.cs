using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculadora.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calculos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    operando_a = table.Column<decimal>(type: "numeric", nullable: false),
                    operando_b = table.Column<decimal>(type: "numeric", nullable: false),
                    operacao = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    resultado = table.Column<decimal>(type: "numeric", nullable: false),
                    criado_em = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calculos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_calculos_criado_em",
                table: "calculos",
                column: "criado_em");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculos");
        }
    }
}
