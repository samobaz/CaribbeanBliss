using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Caribbbeaannn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 17, 8, 25, 799, DateTimeKind.Local).AddTicks(3076));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ClienteReserva");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 16, 17, 15, 100, DateTimeKind.Local).AddTicks(1988));
        }
    }
}
