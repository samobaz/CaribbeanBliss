using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Cariibbbeaann : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 16, 17, 15, 100, DateTimeKind.Local).AddTicks(1988));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 16, 12, 56, 608, DateTimeKind.Local).AddTicks(8882));
        }
    }
}
