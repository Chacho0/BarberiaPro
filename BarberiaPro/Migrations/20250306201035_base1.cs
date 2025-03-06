using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberiaPro.Migrations
{
    /// <inheritdoc />
    public partial class base1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstadoPago",
                table: "CitasProcesadas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Seleccionada",
                table: "CitasProcesadas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoPago",
                table: "CitasProcesadas");

            migrationBuilder.DropColumn(
                name: "Seleccionada",
                table: "CitasProcesadas");
        }
    }
}
