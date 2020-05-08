using static DDDTalk.Dominio.Alunos.Aluno;

namespace DDDTalk.Dominio.Servicos
{
    public interface INotificacaoInscricaoRealizadaComSucesso
    {
        Inscricao EnviarEmail(Inscricao inscricao);
    }
}
