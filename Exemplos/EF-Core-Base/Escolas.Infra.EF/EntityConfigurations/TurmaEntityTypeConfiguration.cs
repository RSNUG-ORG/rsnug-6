using Escola.Dominio.Shared;
using Escola.Dominio.Turmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Escola.Infra.EF.EntityConfigurations
{
    public sealed class TurmaEntityTypeConfiguration : IEntityTypeConfiguration<TurmaBase>
    {
        public void Configure(EntityTypeBuilder<TurmaBase> builder)
        {
            builder.ToTable("Turmas", EscolaContextoEF.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .UseHiLo("turmaseq", EscolaContextoEF.DEFAULT_SCHEMA);
            builder
                .HasDiscriminator<string>("Especializacao")
                .HasValue<TurmaComDuracao>(nameof(TurmaComDuracao))
                .HasValue<TurmaComDuracaoIlimitada>(nameof(TurmaComDuracaoIlimitada));

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(100)")
                .HasConversion<string>(p => p, p => Descricao.Criar(p).Value);
            builder.OwnsOne(p => p.Configuracao, configuracao =>
            {
                configuracao.Property(n => n.LimiteIdade)
                    .HasColumnName("LimiteIdade")
                    .HasColumnType("int")
                    .HasConversion<int>(p => p, p => Quantidade.Criar(p).Value);
                configuracao.OwnsOne(p => p.QuantidadeAlunos, quantidade =>
                 {
                     quantidade.Property(c=> c.Minimo)
                        .HasColumnName("QuantidadeMinimaAlunos")
                        .HasColumnType("int")
                        .HasConversion<int>(p => p, p => Quantidade.Criar(p).Value);
                     quantidade.Property(c => c.Maximo)
                        .HasColumnName("QuantidadeMaximaAlunos")
                        .HasColumnType("int")
                        .HasConversion<int>(p => p, p => Quantidade.Criar(p).Value);
                 });
            });
            builder.Property(c => c.TotalInscritos)
                .HasColumnType("int")
                .HasConversion<int>(p => p, p => Quantidade.Criar(p).Value);
        }
    }

    public sealed class TurmaComDuracaoEntityTypeConfiguration : IEntityTypeConfiguration<TurmaComDuracao>
    {
        public void Configure(EntityTypeBuilder<TurmaComDuracao> builder)
        {
            builder.HasBaseType<TurmaBase>();

            
            builder.OwnsOne(p => p.Duracao, duracao =>
            {
                duracao.Property(n => n.Tipo)
                    .HasColumnName("DuracaoTipo")
                    .HasColumnType("varchar(20)")
                    .HasConversion(new EnumToStringConverter<EDuracaoEm>());
                    
                duracao.Property(n => n.Quantidade)
                    .HasColumnName("DuracaoQuantidade")
                    .HasColumnType("int")
                    .HasConversion<int>(p => p, p => Quantidade.Criar(p).Value);
            });
        }
    }

    public sealed class TurmaComDuracaoIlimitadaEntityTypeConfiguration : IEntityTypeConfiguration<TurmaComDuracaoIlimitada>
    {
        public void Configure(EntityTypeBuilder<TurmaComDuracaoIlimitada> builder)
        {
            builder.HasBaseType<TurmaBase>();
        }
    }
}
