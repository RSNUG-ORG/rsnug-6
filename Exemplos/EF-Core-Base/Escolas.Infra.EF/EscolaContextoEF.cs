using Escola.Dominio.Alunos;
using Escola.Infra.EF.EntityConfigurations;
using Escola.Dominio.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;
using Escola.Dominio.Turmas;

namespace Escola.Infra.EF
{
    public class EscolaContextoEF : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Matriculas";

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<TurmaBase> Turmas { get; set; }

        public EscolaContextoEF(DbContextOptions<EscolaContextoEF> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("EscolaContextoEF::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaComDuracaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaComDuracaoIlimitadaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InscricaoEntityTypeConfiguration());
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
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
