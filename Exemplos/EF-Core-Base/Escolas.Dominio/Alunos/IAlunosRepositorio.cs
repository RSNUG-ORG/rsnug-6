using CSharpFunctionalExtensions;
using Escola.Dominio.Shared;
using System.Threading.Tasks;

namespace Escola.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Task<Aluno> AdicionarAsync(Aluno aluno);
        Task<Maybe<Aluno>> RecuperarPorEmailAsync(Email email);
        //Aluno Atualizar(Aluno aluno);
        //Resultado<Aluno, Falha> RecuperarPorEmail(string email);
        //Resultado<Aluno, Falha> Recuperar(string id);
    }
}
