using CSharpFunctionalExtensions;
using Escola.Dominio.Shared;

namespace Escola.Dominio.Turmas
{
    public abstract class TurmaBase : Entity
    {
        protected TurmaBase() { }
        protected TurmaBase(long id, Descricao descricao, ConfiguracaoInscricao configuracao, Quantidade totalInscritos)
            : base(id)
        {
            Descricao = descricao;
            Configuracao = configuracao;
            TotalInscritos = totalInscritos;
        }

        public Descricao Descricao { get; }
        public ConfiguracaoInscricao Configuracao { get; }
        public Quantidade TotalInscritos { get; private set; }
        public Result<Quantidade> VagasDisponiveis => Quantidade.Criar(Configuracao.QuantidadeAlunos.Maximo - TotalInscritos);

        public void IncrementarInscricoes()
            => TotalInscritos++;
    }


    public sealed class TurmaComDuracao : TurmaBase
    {
        private TurmaComDuracao() : base() { } 
        private TurmaComDuracao(long id, Descricao descricao, ConfiguracaoInscricao configuracao, Quantidade totalInscritos, DuracaoTurma duracao)
            : base(id, descricao, configuracao, totalInscritos)
        {
            Duracao = duracao;
        }

        public DuracaoTurma Duracao { get; }

        public static Result<TurmaComDuracao> Criar(string descricao, int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos, EDuracaoEm tipoDuracao, int duracao)
        {
            var descricaoResultado = Descricao.Criar(descricao);
            var configuracaoResultado = ConfiguracaoInscricao.Criar(limiteIdade, quantidadeMinimaAlunos, quantidadeMaximaAlunos);
            var duracaoResultado = DuracaoTurma.Criar(tipoDuracao, duracao);
            if (Result.Combine(descricaoResultado, configuracaoResultado, duracaoResultado) is var resultado && resultado.IsFailure)
                return Result.Failure<TurmaComDuracao>(resultado.Error);
            return Result.Ok(new TurmaComDuracao(0, descricaoResultado.Value, configuracaoResultado.Value, Quantidade.Criar(0).Value, duracaoResultado.Value));
        }
    }

    public sealed class TurmaComDuracaoIlimitada : TurmaBase
    {
        private TurmaComDuracaoIlimitada() : base() { }
        private TurmaComDuracaoIlimitada(long id, Descricao descricao, ConfiguracaoInscricao configuracao, Quantidade totalInscritos)
            : base(id, descricao, configuracao, totalInscritos)
        { }

        public static Result<TurmaComDuracaoIlimitada> Criar(string descricao, int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos)
        {
            var descricaoResultado = Descricao.Criar(descricao);
            var configuracaoResultado = ConfiguracaoInscricao.Criar(limiteIdade, quantidadeMinimaAlunos, quantidadeMaximaAlunos);            
            if (Result.Combine(descricaoResultado, configuracaoResultado) is var resultado && resultado.IsFailure)
                return Result.Failure<TurmaComDuracaoIlimitada>(resultado.Error);
            return Result.Ok(new TurmaComDuracaoIlimitada(0, descricaoResultado.Value, configuracaoResultado.Value, Quantidade.Criar(0).Value));
        }
    }
}
