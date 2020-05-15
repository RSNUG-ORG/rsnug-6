using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class alunos_controle_criacao_alteracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                schema: "Matriculas",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                schema: "Matriculas",
                table: "Alunos");
        }
    }
}
