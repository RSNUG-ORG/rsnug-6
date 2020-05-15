using Escola.Dominio.Alunos;
using System;

namespace Escolas.Infra.Dapper.POCO
{
    public class AlunoPoco
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }

        public AlunoPoco(int id, string nome, string sobrenome, string email, DateTime dataNascimento, string sexo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public Aluno BuildAluno()
        {
            Aluno aluno = Aluno.Criar(Nome, Sobrenome, Email, DataNascimento, Sexo).Value;
            return aluno.DefinirId(Id);
        }
    }
}
