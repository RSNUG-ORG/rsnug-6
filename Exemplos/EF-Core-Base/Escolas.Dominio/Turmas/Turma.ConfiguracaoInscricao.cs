using CSharpFunctionalExtensions;
using Escola.Dominio.Shared;
using System.Collections.Generic;

namespace Escola.Dominio.Turmas
{
    public sealed class ConfiguracaoInscricao : ValueObject
    {
        private ConfiguracaoInscricao() { }
        public ConfiguracaoInscricao(Quantidade limiteIdade, IntervaloQuantidade quantidadeAlunos)
        {
            LimiteIdade = limiteIdade;
            QuantidadeAlunos = quantidadeAlunos;
        }

        public Quantidade LimiteIdade { get; }
        public IntervaloQuantidade QuantidadeAlunos { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LimiteIdade;
            yield return QuantidadeAlunos;
        }

        public static Result<ConfiguracaoInscricao> Criar(int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos)
        {
            var limiteResultado = Quantidade.Criar(limiteIdade);
            var intervaloResultado = IntervaloQuantidade.Criar(quantidadeMinimaAlunos, quantidadeMaximaAlunos);
            if (Result.Combine(limiteResultado, intervaloResultado) is var resultado && resultado.IsFailure)
                return Result.Failure<ConfiguracaoInscricao>(resultado.Error);
            return Result.Ok(new ConfiguracaoInscricao(limiteResultado.Value, intervaloResultado.Value));
        }
    }
}
