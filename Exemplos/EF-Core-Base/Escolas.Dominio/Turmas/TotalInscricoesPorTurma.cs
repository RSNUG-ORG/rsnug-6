using System;
using System.Collections.Generic;
using System.Text;

namespace Escola.Dominio.Turmas
{
    public sealed class TotalInscricoesPorTurma
    {
        public TotalInscricoesPorTurma(int id, int totalInscritos, int totalReal)
        {
            Id = id;
            TotalInscritos = totalInscritos;
            TotalReal = totalReal;
        }

        public int Id { get;  }
        public int TotalInscritos { get; }
        public int TotalReal { get; }
    }
}
