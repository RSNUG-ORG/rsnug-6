using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class turma_versao_inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Matriculas");

            migrationBuilder.RenameTable(
                name: "Alunos",
                schema: "matriculas",
                newName: "Alunos",
                newSchema: "Matriculas");

            migrationBuilder.RenameSequence(
                name: "pessoaseq",
                schema: "matriculas",
                newName: "Pessoaseq",
                newSchema: "Matriculas");

            migrationBuilder.CreateSequence(
                name: "turmaseq",
                schema: "Matriculas",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Turmas",
                schema: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true),
                    LimiteIdade = table.Column<int>(type: "int", nullable: true),
                    QuantidadeMinimaAlunos = table.Column<int>(type: "int", nullable: true),
                    QuantidadeMaximaAlunos = table.Column<int>(type: "int", nullable: true),
                    TotalInscritos = table.Column<int>(type: "int", nullable: true),
                    Especializacao = table.Column<string>(nullable: false),
                    DuracaoTipo = table.Column<string>(type: "varchar(20)", nullable: true),
                    DuracaoQuantidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turmas",
                schema: "Matriculas");

            migrationBuilder.DropSequence(
                name: "turmaseq",
                schema: "Matriculas");

            migrationBuilder.EnsureSchema(
                name: "matriculas");

            migrationBuilder.RenameTable(
                name: "Alunos",
                schema: "Matriculas",
                newName: "Alunos",
                newSchema: "matriculas");

            migrationBuilder.RenameSequence(
                name: "pessoaseq",
                schema: "Matriculas",
                newName: "pessoaseq",
                newSchema: "matriculas");
        }
    }
}
