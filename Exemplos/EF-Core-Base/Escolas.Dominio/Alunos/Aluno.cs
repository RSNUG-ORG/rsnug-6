using CSharpFunctionalExtensions;
using Escola.Dominio.Shared;
using Escola.Dominio.Turmas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escola.Dominio.Alunos
{
    public sealed class Aluno : Entity
    {
        private Aluno() 
        {
            _inscricoes = new List<Inscricao>();
        }
        private Aluno(long id, NomeAluno nome, Email email, DateTime dataNascimento, ESexo sexo, List<Inscricao> inscricoes)
            : base(id)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            _inscricoes = inscricoes ?? new List<Inscricao>();
        }

        private readonly List<Inscricao> _inscricoes;

        public NomeAluno Nome { get; private set; }
        public Email Email { get; }
        public DateTime DataNascimento { get; private set; }
        public ESexo Sexo { get; }
        public int Idade(DateTime quando) => DataNascimento.GetAge(quando);
        public IReadOnlyCollection<Inscricao> Inscricoes => _inscricoes;

        public static Result<Aluno> Criar(string nome, string sobreNome, string email, DateTime dataNascimento, string sexo)
        {
            var nomeResultado = NomeAluno.Criar(nome, sobreNome);
            var emailResultado = Email.Criar(email);
            var sexoResultado = sexo.ToEnum<ESexo>();
            if (Result.Combine(nomeResultado, emailResultado, sexoResultado) is var resultado && resultado.IsFailure)
                return Result.Failure<Aluno>(resultado.Error);
            if (dataNascimento >= DateTime.Now)
                return Result.Failure<Aluno>("Data de nascimento deve ser menor que hoje");
            return Result.Ok(new Aluno(0, nomeResultado.Value, emailResultado.Value, dataNascimento, sexoResultado.Value, null));
        }

        public Result AtualizarNome(string primeiroNome, string sobrenome)
        {
            var nomeResultado = NomeAluno.Criar(primeiroNome, sobrenome);
            if (nomeResultado.IsFailure)
                return Result.Failure(nomeResultado.Error);
            Nome = nomeResultado.Value;
            return Result.Ok();
        }

        public Result AtualizarDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento.Date >= DateTime.Now.Date)
                return Result.Failure("Data de nascimento dever ser menor que hoje");
            DataNascimento = dataNascimento;
            return Result.Ok();
        }

        public Result RealizarInscricao(TurmaBase turma, DateTime inscricaoEm)
        {
            if (turma.PossoFazerInscrever(this, inscricaoEm) is var resultado && resultado.IsFailure)
                return Result.Failure(resultado.Error);
            _inscricoes.Add( Inscricao.Criar(turma.Id, turma.RecuperarDataEncerramento(inscricaoEm)));
            turma.IncrementarInscricoes();
            return Result.Ok();
        }

        #region Dapper Methods
        public Aluno DefinirId(long id) => new Aluno(id, Nome, Email, DataNascimento, Sexo, null);
        #endregion
        }
    }
}
