using CSharpFunctionalExtensions;
using DDDTalk.Dominio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTalk.Dominio.Alunos
{
    public sealed partial class Aluno : Entity
    {
        private Aluno(long id, NomeAluno nome, Email email, DateTime dataNascimento, ESexo sexo)
            : base(id)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public NomeAluno Nome { get; }
        public Email Email { get;  }        
        public DateTime DataNascimento { get; }
        public ESexo Sexo { get; }
        public int Idade(DateTime quando) => DataNascimento.GetAge(quando);

        public static Result<Aluno> Novo(string nome, string sobreNome, string email, DateTime dataNascimento, ESexo sexo)
        {
            var nomeResultado = NomeAluno.Criar(nome, sobreNome);
            var emailResultado = Email.Criar(email);
            if (Result.Combine(nomeResultado, emailResultado) is var resultado && resultado.IsFailure)
                return Result.Failure<Aluno>(resultado.Error);
            if(dataNascimento >= DateTime.Now)
                return Result.Failure<Aluno>("Data de nascimento deve ser menor que hoje");
            return Result.Ok(new Aluno(0, nomeResultado.Value, emailResultado.Value, dataNascimento, sexo));
        }
    }
}
