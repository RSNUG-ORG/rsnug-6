using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class versao_inicial_alunos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "matriculas");

            migrationBuilder.CreateSequence(
                name: "pessoaseq",
                schema: "matriculas",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Alunos",
                schema: "matriculas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    PrimeiroNome = table.Column<string>(type: "varchar(20)", nullable: true),
                    Sobrenome = table.Column<string>(type: "varchar(40)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos",
                schema: "matriculas");

            migrationBuilder.DropSequence(
                name: "pessoaseq",
                schema: "matriculas");
        }
    }
}
