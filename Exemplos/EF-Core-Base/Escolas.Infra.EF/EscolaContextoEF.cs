using Escola.Dominio.Alunos;
using Escola.Infra.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Escola.Infra.EF
{
    public class EscolaContextoEF : DbContext
    {
        public const string DEFAULT_SCHEMA = "Matriculas";

        public DbSet<Aluno> Alunos { get; set; }

        public EscolaContextoEF(DbContextOptions<EscolaContextoEF> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("EscolaContextoEF::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoEntityTypeConfiguration());
        }
    }

    public class EscolaContextoEFDesignFactory : IDesignTimeDbContextFactory<EscolaContextoEF>
    {
        public EscolaContextoEF CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EscolaContextoEF>()
                .UseSqlServer("Server=.;Initial Catalog=DDDTalk.Infra.OrderingDb;Integrated Security=true");

            return new EscolaContextoEF(optionsBuilder.Options);
        }
    }
}
