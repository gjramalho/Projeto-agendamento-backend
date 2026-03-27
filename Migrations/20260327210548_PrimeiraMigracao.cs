using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoAgendamento.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cliente",
                table: "Agendamentos",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMinutos",
                table: "Agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MotivoCancelamento",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeCliente",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracaoMinutos",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "MotivoCancelamento",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "NomeCliente",
                table: "Agendamentos");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Agendamentos",
                newName: "Cliente");
        }
    }
}
