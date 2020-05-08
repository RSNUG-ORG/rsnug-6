using Microsoft.EntityFrameworkCore;
using System;

namespace DDDTalk.Dominio.Infra.EF
{
    public class EscolaContextoEF : DbContext
    {
        public const string DEFAULT_SCHEMA = "matriculas";

        public EscolaContextoEF(DbContextOptions<EscolaContextoEF> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("EscolaContextoEF::ctor ->" + this.GetHashCode());
        }
    }
}
