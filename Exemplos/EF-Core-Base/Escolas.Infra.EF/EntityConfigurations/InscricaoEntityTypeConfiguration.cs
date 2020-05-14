using Escola.Dominio.Alunos;
using Escola.Dominio.Turmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Escola.Infra.EF.EntityConfigurations
{
    public sealed class InscricaoEntityTypeConfiguration : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable("Inscricoes", EscolaContextoEF.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .UseHiLo("inscricaoseq", EscolaContextoEF.DEFAULT_SCHEMA);
            builder.Property(c => c.InscritoEm)
                .HasColumnType("datetime2");
            builder.Property(c => c.EncerraEm)
                .HasColumnType("datetime2");
            builder.Property(c => c.Situacao)
                .HasColumnType("varchar(20)")
                .HasConversion(new EnumToStringConverter<Inscricao.ESituacao>());
            builder.HasOne(typeof(TurmaBase))
                .WithMany()
                .HasForeignKey("TurmaId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
