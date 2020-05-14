using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class versao_inicial_inscricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "inscricaoseq",
                schema: "Matriculas",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                schema: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    TurmaId = table.Column<long>(nullable: false),
                    InscritoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "varchar(20)", nullable: false),
                    PessoaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Alunos_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "Matriculas",
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalSchema: "Matriculas",
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_PessoaId",
                schema: "Matriculas",
                table: "Inscricoes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_TurmaId",
                schema: "Matriculas",
                table: "Inscricoes",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricoes",
                schema: "Matriculas");

            migrationBuilder.DropSequence(
                name: "inscricaoseq",
                schema: "Matriculas");
        }
    }
}
