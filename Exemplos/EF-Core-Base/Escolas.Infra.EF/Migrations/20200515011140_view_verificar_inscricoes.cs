using Microsoft.EntityFrameworkCore.Migrations;

namespace Escola.Infra.EF.Migrations
{
    public partial class view_verificar_inscricoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE VIEW [matriculas].[VerificarTotalInscricoes] AS
									SELECT matriculas.Turmas.Id, 
	                                    matriculas.Turmas.TotalInscritos,
	                                    (SELECT COUNT(matriculas.Inscricoes.Id) FROM matriculas.Inscricoes 
		                                    WHERE matriculas.Inscricoes.TurmaId = matriculas.Turmas.Id AND matriculas.Inscricoes.Situacao = 'Confirmada') AS TotalReal
	                                    FROM matriculas.Turmas
									GO", true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW [matriculas].[VerificarTotalInscricoes]", true);
		}
    }
}
