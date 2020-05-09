using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Escola.Dominio.Shared
{
    public sealed class NomeAluno : ValueObject
    {
        public NomeAluno(string primeiro, string sobrenome)
        {
            Primeiro = primeiro;
            Sobrenome = sobrenome;
        }

        public string Primeiro { get; }
        public string Sobrenome { get; }
        
        public static Result<NomeAluno> Criar(string nome, string sobrenome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Result.Failure<NomeAluno>("Nome do aluno não pode ser vazio");
            if (string.IsNullOrWhiteSpace(sobrenome))
                return Result.Failure<NomeAluno>("Sobrenome do aluno não pode ser vazio");

            nome = nome.Trim();
            sobrenome = sobrenome.Trim();

            if (nome.Length > 20)
                return Result.Failure<NomeAluno>("Nome do aluno é muito longo");
            if (nome.Length > 40)
                return Result.Failure<NomeAluno>("Sobrenome do aluno é muito longo");
            return Result.Ok(new NomeAluno(nome, sobrenome));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Primeiro;
            yield return Sobrenome;
        }

        public static implicit operator string(NomeAluno nome) => $"{nome.Primeiro} {nome.Sobrenome}";

        public override string ToString() => this;
    }
}
