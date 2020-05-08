using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DDDTalk.Dominio.Shared
{
    public sealed class Email : ValueObject
    {
        private readonly string _valor;

        private Email(string valor)
        {
            _valor = valor;
        }

        public static Result<Email> Criar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>("E-mail não pode ser vazio");

            if (email.Length > 100)
                return Result.Failure<Email>("E-mail é muito longo");

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return Result.Failure<Email>("E-mail é inválido");

            return Result.Ok(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _valor;
        }

        public static implicit operator string(Email email) => email._valor;
    }
}
