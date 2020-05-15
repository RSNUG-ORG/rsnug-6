using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class exclusao_virtual_turma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                schema: "Matriculas",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                schema: "Matriculas",
                table: "Turmas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                schema: "Matriculas",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Excluido",
                schema: "Matriculas",
                table: "Turmas");
        }
    }
}
