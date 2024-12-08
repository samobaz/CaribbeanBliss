using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Caribbbeaannnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ClienteReserva");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 17, 13, 56, 941, DateTimeKind.Local).AddTicks(8297));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClienteReserva");

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
    }
}
