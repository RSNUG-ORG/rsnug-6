using Escola.Dominio.Shared;
using System;

namespace Escola.Dominio.Alunos
{
    public sealed class Inscricao : Entity
    {
        private Inscricao() : base() { }
        private Inscricao(long id, long turmaId, DateTime inscritoEm, DateTime encerraEm, ESituacao situacao)
            : base(id)
        {
            TurmaId = turmaId;
            InscritoEm = inscritoEm;
            EncerraEm = encerraEm;
            Situacao = situacao;
        }

        public long TurmaId { get; }
        public DateTime InscritoEm { get; }
        public DateTime EncerraEm { get; }
        public ESituacao Situacao { get; }

        public enum ESituacao
        {
            Confirmada,
            Encerrada
        }

        internal static Inscricao Criar(long turmaId, DateTime encerraEm)
            => new Inscricao(0, turmaId, DateTime.UtcNow, encerraEm, ESituacao.Confirmada);
        
    }

}
