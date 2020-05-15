using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Escola.Infra.EF.EntityConfigurations
{
    public sealed class AlunoEntityTypeConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos", EscolaContextoEF.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .UseHiLo("pessoaseq", EscolaContextoEF.DEFAULT_SCHEMA);
            builder.OwnsOne(p => p.Nome, nome =>
            {
                nome.WithOwner();
                nome.Property(n => n.Primeiro).HasColumnName("PrimeiroNome").HasColumnType("varchar(20)");
                nome.Property(n => n.Sobrenome).HasColumnName("Sobrenome").HasColumnType("varchar(40)");
            });
            builder.Property(p => p.Email)
                .HasColumnType("varchar(100)")
                .HasConversion<string>(p => p, p => Email.Criar(p).Value);
            builder.Property(p => p.DataNascimento)
                .HasColumnType("date");
            builder.Property(p => p.Sexo)
                .HasColumnType("varchar(15)")
                .HasConversion(new EnumToStringConverter<ESexo>());
            builder
                .HasMany(c => c.Inscricoes)
                .WithOne()
                .HasForeignKey("PessoaId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_inscricoes");
        }
    }
}
