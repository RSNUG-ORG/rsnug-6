using CSharpFunctionalExtensions;
using Dapper;
using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
using Escolas.Infra.Dapper.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Escolas.Infra.Dapper.Repositorios
{
    public class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly IDbConnection _connection;

        public AlunosRepositorio(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Aluno> AdicionarAsync(Aluno aluno)
        {

            string query = QueryReader.GetQuery("Queries", "AdicionarAluno");
            long insertedId = await _connection.ExecuteScalarAsync<long>(query, aluno);
            return aluno.DefinirId(insertedId);
        }

        public async Task<Maybe<Aluno>> RecuperarAsync(long id)
        {
            string query = QueryReader.GetQuery("Queries", "Recuperar");
            AlunoPoco aluno = await _connection.QueryFirstOrDefaultAsync<AlunoPoco>(query, new { id });
            return Maybe<Aluno>.From(aluno.BuildAluno());
        }

        //Exemplo de recuperação no braço
        public async Task<Maybe<Aluno>> RecuperarPorEmailAsync(Email email)
        {
            string query = QueryReader.GetQuery("Queries", "RecuperarAlunoPorEmail");
            IEnumerable<Aluno> alunoByEmail = await _connection.QueryAsync<long, string, string, string, DateTime, string, Aluno>(query, (id, primeiroNome, sobreNome, email, dataNascimento, sexo) =>
            {
                Aluno recoveredAluno = Aluno.Criar(primeiroNome, sobreNome, email, dataNascimento, sexo).Value.DefinirId(id);
                return recoveredAluno;
            });

            return alunoByEmail.TryFirst();
        }
    }
}
