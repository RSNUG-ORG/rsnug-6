using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Escola.Dominio.Turmas
{
    public sealed class Descricao : ValueObject
    {
        private readonly string _valor;

        private Descricao(string valor)
        {
            _valor = valor;
        }

        public static Result<Descricao> Criar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Descricao>("Descrição não pode ser vazia");

            if (email.Length > 100)
                return Result.Failure<Descricao>("Descrição é muito longa");

            return Result.Ok(new Descricao(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _valor;
        }

        public static implicit operator string(Descricao descricao) => descricao._valor;
    }
}
