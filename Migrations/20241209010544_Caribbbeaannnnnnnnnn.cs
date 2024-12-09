using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caribbean2.Migrations
{
    /// <inheritdoc />
    public partial class Caribbbeaannnnnnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroHabitacion",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HabitacionesDisponibles",
                table: "Habitaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TipodeHabitacion",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Habitaciones",
                keyColumn: "IdHabitacion",
                keyValue: 1,
                column: "HabitacionesDisponibles",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Habitaciones",
                keyColumn: "IdHabitacion",
                keyValue: 2,
                column: "HabitacionesDisponibles",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Habitaciones",
                keyColumn: "IdHabitacion",
                keyValue: 3,
                column: "HabitacionesDisponibles",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Habitaciones",
                keyColumn: "IdHabitacion",
                keyValue: 4,
                column: "HabitacionesDisponibles",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 20, 5, 43, 979, DateTimeKind.Local).AddTicks(5850));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroHabitacion",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "HabitacionesDisponibles",
                table: "Habitaciones");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipodeHabitacion",
                table: "ClienteReserva",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioID",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2024, 12, 8, 17, 31, 7, 136, DateTimeKind.Local).AddTicks(9988));
        }
    }
}
