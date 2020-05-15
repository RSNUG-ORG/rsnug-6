using CSharpFunctionalExtensions;
using Dapper;
using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
using Escolas.Infra.Dapper.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Escolas.Infra.Dapper.Repositorios
{
    public class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly IDbConnection _connection;
        const string _alunoBasePath = @"Queries\Alunos";

        public AlunosRepositorio(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Aluno> AdicionarAsync(Aluno aluno)
        {
            string query = QueryReader.GetQuery(_alunoBasePath, "AdicionarAluno");

            using (var tran = _connection.BeginTransaction())
            {
                AlunoPoco alunoToInsert = new AlunoPoco(aluno);
                long insertedId = await _connection.ExecuteScalarAsync<long>(query, alunoToInsert);

                if (insertedId > 0)
                {
                    query = QueryReader.GetQuery(_alunoBasePath, "AdicionarInscricoes");
                    await _connection.ExecuteAsync(query, aluno.Inscricoes);
                }

                if (insertedId > 0)
                    tran.Commit();
                else
                    tran.Rollback();

                return aluno.DefinirId(insertedId);
            }
        }

        public async void AtualizarAsync(Aluno aluno)
        {
            string query = QueryReader.GetQuery(_alunoBasePath, "AtualizarAluno");
            await _connection.ExecuteAsync(query, aluno);
        }

        public async Task<Maybe<Aluno>> RecuperarAsync(long id)
        {
            string query = QueryReader.GetQuery(_alunoBasePath, "Recuperar");

            AlunoPoco aluno;
            using (var alunoQuery = await _connection.QueryMultipleAsync(query, new { id }))
            {
                aluno = alunoQuery.Read<AlunoPoco>().FirstOrDefault();
                if (aluno != null)
                {
                    aluno.Inscricoes = alunoQuery.Read<InscricaoPoco>().ToList();
                }
            }
            return Maybe<Aluno>.From(aluno.BuildAluno());
        }

        //Exemplo de recuperação no braço
        public async Task<Maybe<Aluno>> RecuperarPorEmailAsync(Email email)
        {
            string query = QueryReader.GetQuery(_alunoBasePath, "RecuperarAlunoPorEmail");
            IEnumerable<Aluno> alunoByEmail = await _connection.QueryAsync<long, string, string, string, DateTime, string, Aluno>(query, (id, primeiroNome, sobreNome, email, dataNascimento, sexo) =>
            {
                Aluno recoveredAluno = Aluno.Criar(primeiroNome, sobreNome, email, dataNascimento, sexo).Value.DefinirId(id);
                return recoveredAluno;
            });

            return alunoByEmail.TryFirst();
        }
    }
}
