namespace DDDTalk.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Aluno IncluirESalvar(Aluno aluno);
        //Aluno Atualizar(Aluno aluno);
        //Resultado<Aluno, Falha> RecuperarPorEmail(string email);
        //Resultado<Aluno, Falha> Recuperar(string id);
    }
}
