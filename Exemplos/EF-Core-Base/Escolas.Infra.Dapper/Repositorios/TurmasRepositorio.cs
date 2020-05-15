using CSharpFunctionalExtensions;
using Dapper;
using Escola.Dominio.Turmas;
using Escolas.Infra.Dapper.POCO;
using System.Data;
using System.Threading.Tasks;

namespace Escolas.Infra.Dapper.Repositorios
{
    public class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly IDbConnection _connection;

        public TurmasRepositorio(IDbConnection connection) => _connection = connection;

        public async Task<TurmaBase> AdicionarAsync(TurmaBase turma)
        {
            string query = QueryReader.GetQuery("Queries\\Turmas", "Adicionar");
            long insertedId = await _connection.ExecuteScalarAsync<long>(query, turma);
            return turma.DefinirId(insertedId);
        }

        public async Task<Maybe<TurmaBase>> RecuperarAsync(long id)
        {
            string query = QueryReader.GetQuery("Queries\\Turmas", "Recuperar");
            TurmasPoco turma = await _connection.QueryFirstOrDefaultAsync<TurmasPoco>(query, new { id });
            return Maybe<TurmaBase>.From(turma.BuildTurma());
        }
    }
}
