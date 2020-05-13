using CSharpFunctionalExtensions;
using Dapper;
using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
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

        public Task<Aluno> AdicionarAsync(Aluno aluno)
        {

            string query = QueryReader.GetQuery("Queries", "AdicionarAluno");
            Task<long> insertedId = _connection.ExecuteScalarAsync<long>(query, aluno);
            return insertedId.ContinueWith((id) => aluno.DefinirId(id.Result));            
        }

        public Task<Maybe<Aluno>> RecuperarAsync(long id)
        {
            //TODO: Utilizar mapeamento aqui
            throw new NotImplementedException();
        }

        public Task<Maybe<Aluno>> RecuperarPorEmailAsync(Email email)
        {
            string query = QueryReader.GetQuery("Queries", "RecuperarAlunoPorEmail");
            Task<IEnumerable<Aluno>> alunoByEmail = _connection.QueryAsync<long, string, string, string, DateTime, string, Aluno>(query, (id, primeiroNome, sobreNome, email, dataNascimento, sexo) =>
            {
                Aluno recoveredAluno = Aluno.Criar(primeiroNome, sobreNome, email, dataNascimento, sexo).Value.DefinirId(id);
                return recoveredAluno;
            });
            return alunoByEmail.ContinueWith((alunos) =>  alunos.Result.TryFirst());            
        }
    }
}
