using CSharpFunctionalExtensions;
using Dapper;
using Escola.Dominio.Turmas;
using Escolas.Infra.Dapper.POCO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Escolas.Infra.Dapper.Repositorios
{
    public class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly IDbConnection _connection;

        const string _turmaBasePath = @"Queries\Turmas";

        public TurmasRepositorio(IDbConnection connection) => _connection = connection;

        public async Task<TurmaBase> AdicionarAsync(TurmaBase turma)
        {
            string query = QueryReader.GetQuery(_turmaBasePath, "Adicionar");
            long insertedId = await _connection.ExecuteScalarAsync<long>(query, turma);
            return turma.DefinirId(insertedId);
        }

        public async void AtualizarAsync(TurmaBase turma)
        {
            string query = QueryReader.GetQuery(_turmaBasePath, "Atualizar");
            await _connection.ExecuteScalarAsync(query, turma);
        }

        public async Task<Maybe<TurmaBase>> RecuperarAsync(long id)
        {
            string query = QueryReader.GetQuery(_turmaBasePath, "Recuperar");
            TurmasPoco turma = await _connection.QueryFirstOrDefaultAsync<TurmasPoco>(query, new { id });
            return Maybe<TurmaBase>.From(turma.BuildTurma());
        }

        public async Task<IEnumerable<TotalInscricoesPorTurma>> RecuperarDiferencasDeInscricoesAsync()
        {
            string query = QueryReader.GetQuery(_turmaBasePath, "RecuperarDiferencasDeInscricoes");
            IEnumerable<TotalInscricoesPorTurma> diferencas = await _connection.QueryAsync<TotalInscricoesPorTurma>(query);
            return diferencas;
        }
    }
}
