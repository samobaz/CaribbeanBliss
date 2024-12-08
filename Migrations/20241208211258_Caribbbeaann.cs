using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Caribbbeaann : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 16, 12, 56, 608, DateTimeKind.Local).AddTicks(8882));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 14, 39, 48, 641, DateTimeKind.Local).AddTicks(2922));
        }
    }
}
