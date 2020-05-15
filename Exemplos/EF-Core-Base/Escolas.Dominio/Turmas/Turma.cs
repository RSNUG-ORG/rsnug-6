using CSharpFunctionalExtensions;
using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
using System;

namespace Escola.Dominio.Turmas
{
    public abstract class TurmaBase : Entity
    {
        protected TurmaBase() { }
        protected TurmaBase(long id, Descricao descricao, ConfiguracaoInscricao configuracao, Quantidade totalInscritos, bool excluido = false)
            : base(id)
        {
            Descricao = descricao;
            Configuracao = configuracao;
            TotalInscritos = totalInscritos;
            Excluido = excluido;
        }

        public Descricao Descricao { get; }
        public ConfiguracaoInscricao Configuracao { get; }
        public Quantidade TotalInscritos { get; private set; }
        public bool Excluido { get; private set; }
        public Result<Quantidade> VagasDisponiveis => Quantidade.Criar(Configuracao.QuantidadeAlunos.Maximo - TotalInscritos);


        public void IncrementarInscricoes()
            => TotalInscritos++;

        internal Result PossoFazerInscrever(Aluno aluno, DateTime inscricaoEm)
        {
            if(VagasDisponiveis.IsFailure)            
                return Result.Failure("Quantidade máxima de alunos ultrapassada");
            if(aluno.Idade(DateTime.UtcNow) > Configuracao.LimiteIdade)
                return Result.Failure("Aluno não possui idade para a turma desejada");
            return Result.Ok();
        }

        internal abstract DateTime RecuperarDataEncerramento(DateTime dataInicio);

        public void Excluir()
        {
            Excluido = true;
        }

        public static Result<TurmaBase> CriarComDuracao(string descricao, int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos, string tipoDuracao, int duracao)
        {
            if (tipoDuracao.ToEnum<EDuracaoEm>() is var tipo && tipo.IsFailure)
                return Result.Failure<TurmaBase>(tipo.Error);
            if(TurmaComDuracao.Criar(descricao, limiteIdade, quantidadeMinimaAlunos, quantidadeMaximaAlunos, tipo.Value, duracao) is var turma && turma.IsFailure)
                return Result.Failure<TurmaBase>(turma.Error);
            return Result.Ok(turma.Value as TurmaBase);
        }

        public static Result<TurmaBase> CriarComDuracaoIlimitada(string descricao, int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos)
        {
            if (TurmaComDuracaoIlimitada.Criar(descricao, limiteIdade, quantidadeMinimaAlunos, quantidadeMaximaAlunos) is var turma && turma.IsFailure)
                return Result.Failure<TurmaBase>(turma.Error);
            return Result.Ok(turma.Value as TurmaBase);
        }
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

        internal override DateTime RecuperarDataEncerramento(DateTime dataInicio)
            => Duracao.Tipo switch
            {
                EDuracaoEm.Anos => dataInicio.AddYears(Duracao.Quantidade),
                EDuracaoEm.Meses => dataInicio.AddMonths(Duracao.Quantidade),
                _ => dataInicio.AddDays(Duracao.Quantidade)
            };

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

        internal override DateTime RecuperarDataEncerramento(DateTime dataInicio)
            => DateTime.MaxValue;

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
