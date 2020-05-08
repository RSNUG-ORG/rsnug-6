using System;

namespace Escola.Dominio.Dividas
{
    public sealed class Divida
    {
        public Divida(string id, string turmaId, string alunoId, DateTime vencimento, decimal valor)
        {
            Id = id;
            TurmaId = turmaId;
            AlunoId = alunoId;
            Vencimento = vencimento;
            Valor = valor;
        }

        public string Id { get; }
        public string TurmaId { get;  }
        public string AlunoId { get; }
        public DateTime Vencimento { get; }
        public decimal Valor { get; }

        public static Divida Nova(string turmaId, string alunoId, DateTime vencimento, decimal valor)
            => new Divida(Guid.NewGuid().ToString(), turmaId, alunoId, vencimento, valor);
    }
}
