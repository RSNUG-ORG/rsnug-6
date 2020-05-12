using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Escola.Dominio.Shared
{
    public sealed class Quantidade: ValueObject
    {
        private readonly int _valor;

        private Quantidade(int valor)
        {
            _valor = valor;
        }

        private Quantidade Incrementar(int valor)
        {
            if (Quantidade.Criar(valor) is var incrementar && incrementar.IsFailure)
                throw new System.Exception(incrementar.Error);
            return Incrementar(incrementar.Value);
        }

        public Quantidade Incrementar(Quantidade valor)
            => new Quantidade(_valor + valor);

        public static Result<Quantidade> Criar(int valor)
        {
            if (valor < 0)
                return Result.Failure<Quantidade>("Limite deve ser maior que zero");

            return Result.Ok(new Quantidade(valor));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _valor;
        }

        public static implicit operator int(Quantidade limite) => limite._valor;
        public static int operator -(Quantidade esq, Quantidade dir) => esq._valor - dir._valor;
        public static int operator +(Quantidade esq, Quantidade dir) => esq._valor + dir._valor;
        public static Quantidade operator ++(Quantidade esq) => esq.Incrementar(1);
        public static Quantidade operator --(Quantidade esq) => esq.Incrementar(1);
        public static int operator *(Quantidade esq, int dir) => esq._valor * dir;
        public static int operator /(Quantidade esq, int dir) => esq._valor / dir;
    }
}
