using CSharpFunctionalExtensions;
using Escola.Dominio.Turmas;
using System;

namespace Escolas.Infra.Dapper.POCO
{
    public class TurmasPoco
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public int LimiteIdade { get; set; }
        public int QuantidadeMinimaAlunos { get; set; }
        public int QuantidadeMaximaAlunos { get; set; }
        public int TotalInscritos { get; set; }

        #region Turmas com duracao
        public string DuracaoTipo { get; set; }
        public int DuracaoQuantidade { get; set; }
        #endregion

        public TurmasPoco(long id, string descricao, int limiteIdade, int quantidadeMinimaAlunos, int quantidadeMaximaAlunos, int totalInscritos, string duracaoTipo, int duracaoQuantidade)
        {
            Id = id;
            Descricao = descricao;
            LimiteIdade = limiteIdade;
            QuantidadeMinimaAlunos = quantidadeMinimaAlunos;
            QuantidadeMaximaAlunos = quantidadeMaximaAlunos;
            TotalInscritos = totalInscritos;
            DuracaoTipo = duracaoTipo;
            DuracaoQuantidade = duracaoQuantidade;
        }

        public TurmaBase BuildTurma()
        {
            Result<TurmaBase> turma;
            if (Enum.IsDefined(typeof(EDuracaoEm), DuracaoTipo) && DuracaoQuantidade > 0)
                turma = TurmaBase.CriarComDuracao(Descricao, LimiteIdade, QuantidadeMinimaAlunos, QuantidadeMaximaAlunos, DuracaoTipo, DuracaoQuantidade);
            else
                turma = TurmaBase.CriarComDuracaoIlimitada(Descricao, LimiteIdade, QuantidadeMinimaAlunos, QuantidadeMaximaAlunos);

            return turma.Value.DefinirId(Id);
        }
    }
}
