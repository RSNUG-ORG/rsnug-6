using CSharpFunctionalExtensions;
using Escola.Dominio.Shared;
using System.Collections.Generic;

namespace Escola.Dominio.Turmas
{
    public sealed class DuracaoTurma: ValueObject
    {
        private DuracaoTurma(EDuracaoEm tipo, Quantidade quantidade)
        {
            Tipo = tipo;
            Quantidade = quantidade;
        }

        public EDuracaoEm Tipo { get; }
        public Quantidade Quantidade { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Tipo;
            yield return Quantidade;
        }

        public static Result<DuracaoTurma> CriarParaDias(int quantidade)
            => Criar(EDuracaoEm.Dias, quantidade);

        public static Result<DuracaoTurma> CriarParaMeses(int quantidade)
            => Criar(EDuracaoEm.Meses, quantidade);

        public static Result<DuracaoTurma> CriarParaAnos(int quantidade)
            => Criar(EDuracaoEm.Anos, quantidade);

        public static Result<DuracaoTurma> Criar(EDuracaoEm tipo, int quantidade)
        {
            if (Quantidade.Criar(quantidade) is var valor && valor.IsFailure)
                return Result.Failure<DuracaoTurma>(valor.Error);
            return Result.Ok(new DuracaoTurma(tipo, valor.Value));
        }
    }
}
