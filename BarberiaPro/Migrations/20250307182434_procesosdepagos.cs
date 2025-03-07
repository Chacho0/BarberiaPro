using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberiaPro.Migrations
{
    /// <inheritdoc />
    public partial class procesosdepagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroTransferencia",
                table: "CitasProcesadas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "CitasProcesadas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherPath",
                table: "CitasProcesadas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroTransferencia",
                table: "CitasProcesadas");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "CitasProcesadas");

            migrationBuilder.DropColumn(
                name: "VoucherPath",
                table: "CitasProcesadas");
        }
    }
}
