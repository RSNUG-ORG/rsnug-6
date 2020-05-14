using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Escola.Dominio.Shared
{
    public sealed class IntervaloQuantidade : ValueObject
    {
        private IntervaloQuantidade(Quantidade minimo, Quantidade maximo)
        {
            Minimo = minimo;
            Maximo = maximo;
        }

        public Quantidade Minimo { get; }
        public Quantidade Maximo { get; }

        public static Result<IntervaloQuantidade> Criar(int minima, int maxima)
        {
            var minimaResultado = Quantidade.Criar(minima);
            var maximaResultado = Quantidade.Criar(maxima);
            var intervaloResultado = Result.FailureIf(() => minima > maxima, "Quantidade mínima deve ser menor que máxima");
            var resultado = Result.Combine(minimaResultado, maximaResultado, intervaloResultado);
            if (resultado.IsFailure)
                return Result.Failure<IntervaloQuantidade>(resultado.Error);
            return Result.Ok(new IntervaloQuantidade(minimaResultado.Value, maximaResultado.Value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Minimo;
            yield return Maximo;
        }
    }
}
