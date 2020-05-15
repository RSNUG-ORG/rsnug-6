using Escola.Dominio.Alunos;
using Escola.Infra.EF.EntityConfigurations;
using Escola.Dominio.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;
using Escola.Dominio.Turmas;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Escola.Infra.EF
{
    public class EscolaContextoEF : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Matriculas";

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<TurmaBase> Turmas { get; set; }
        public DbSet<TotalInscricoesPorTurma> ViewVericacaoInscricoes { get; set; }

        public EscolaContextoEF(DbContextOptions<EscolaContextoEF> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("EscolaContextoEF::ctor ->" + this.GetHashCode());
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaComDuracaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaComDuracaoIlimitadaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InscricaoEntityTypeConfiguration());
            modelBuilder.Entity<TotalInscricoesPorTurma>(e => e.ToView("VerificarTotalInscricoes", DEFAULT_SCHEMA).HasNoKey());
        }

        private ILoggerFactory GetLoggerFactory()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
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
