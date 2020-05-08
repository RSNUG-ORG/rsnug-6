using static Escola.Dominio.Alunos.Aluno;

namespace Escola.Dominio.Servicos
{
    public interface INotificacaoInscricaoRealizadaComSucesso
    {
        Inscricao EnviarEmail(Inscricao inscricao);
    }
}
