using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Caribbbeaannnnnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ClienteReserva",
                newName: "UsuarioId");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 17, 31, 7, 136, DateTimeKind.Local).AddTicks(9988));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ClienteReserva",
                newName: "UserId");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 17, 13, 56, 941, DateTimeKind.Local).AddTicks(8297));
        }
    }
}
