﻿// <auto-generated />
using System;
using Escola.Infra.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Escola.Infra.EF.Migrations
{
    [DbContext(typeof(EscolaContextoEF))]
    partial class EscolaContextoEFModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:Matriculas.inscricaoseq", "'inscricaoseq', 'Matriculas', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:Matriculas.pessoaseq", "'pessoaseq', 'Matriculas', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:Matriculas.turmaseq", "'turmaseq', 'Matriculas', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Escola.Dominio.Alunos.Aluno", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "pessoaseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Matriculas")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Alunos","Matriculas");
                });

            modelBuilder.Entity("Escola.Dominio.Alunos.Inscricao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "inscricaoseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Matriculas")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("EncerraEm")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InscritoEm")
                        .HasColumnType("datetime2");

                    b.Property<long?>("PessoaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<long>("TurmaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Inscricoes","Matriculas");
                });

            modelBuilder.Entity("Escola.Dominio.Turmas.TurmaBase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "turmaseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Matriculas")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Especializacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("RowVersion")
                        .HasColumnType("rowversion");

                    b.Property<int?>("TotalInscritos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Turmas","Matriculas");

                    b.HasDiscriminator<string>("Especializacao").HasValue("TurmaBase");
                });

            modelBuilder.Entity("Escola.Dominio.Turmas.TurmaComDuracao", b =>
                {
                    b.HasBaseType("Escola.Dominio.Turmas.TurmaBase");

                    b.HasDiscriminator().HasValue("TurmaComDuracao");
                });

            modelBuilder.Entity("Escola.Dominio.Turmas.TurmaComDuracaoIlimitada", b =>
                {
                    b.HasBaseType("Escola.Dominio.Turmas.TurmaBase");

                    b.HasDiscriminator().HasValue("TurmaComDuracaoIlimitada");
                });

            modelBuilder.Entity("Escola.Dominio.Alunos.Aluno", b =>
                {
                    b.OwnsOne("Escola.Dominio.Shared.NomeAluno", "Nome", b1 =>
                        {
                            b1.Property<long>("AlunoId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Primeiro")
                                .HasColumnName("PrimeiroNome")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("Sobrenome")
                                .HasColumnName("Sobrenome")
                                .HasColumnType("varchar(40)");

                            b1.HasKey("AlunoId");

                            b1.ToTable("Alunos");

                            b1.WithOwner()
                                .HasForeignKey("AlunoId");
                        });
                });

            modelBuilder.Entity("Escola.Dominio.Alunos.Inscricao", b =>
                {
                    b.HasOne("Escola.Dominio.Alunos.Aluno", null)
                        .WithMany("Inscricoes")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Escola.Dominio.Turmas.TurmaBase", null)
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Escola.Dominio.Turmas.TurmaBase", b =>
                {
                    b.OwnsOne("Escola.Dominio.Turmas.ConfiguracaoInscricao", "Configuracao", b1 =>
                        {
                            b1.Property<long>("TurmaBaseId")
                                .HasColumnType("bigint");

                            b1.Property<int?>("LimiteIdade")
                                .HasColumnName("LimiteIdade")
                                .HasColumnType("int");

                            b1.Property<byte[]>("RowVersion")
                                .IsConcurrencyToken()
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnName("RowVersion")
                                .HasColumnType("rowversion");

                            b1.HasKey("TurmaBaseId");

                            b1.ToTable("Turmas");

                            b1.WithOwner()
                                .HasForeignKey("TurmaBaseId");

                            b1.OwnsOne("Escola.Dominio.Shared.IntervaloQuantidade", "QuantidadeAlunos", b2 =>
                                {
                                    b2.Property<long>("ConfiguracaoInscricaoTurmaBaseId")
                                        .HasColumnType("bigint");

                                    b2.Property<int?>("Maximo")
                                        .HasColumnName("QuantidadeMaximaAlunos")
                                        .HasColumnType("int");

                                    b2.Property<int?>("Minimo")
                                        .HasColumnName("QuantidadeMinimaAlunos")
                                        .HasColumnType("int");

                                    b2.Property<byte[]>("RowVersion")
                                        .IsConcurrencyToken()
                                        .ValueGeneratedOnAddOrUpdate()
                                        .HasColumnName("RowVersion")
                                        .HasColumnType("rowversion");

                                    b2.HasKey("ConfiguracaoInscricaoTurmaBaseId");

                                    b2.ToTable("Turmas");

                                    b2.WithOwner()
                                        .HasForeignKey("ConfiguracaoInscricaoTurmaBaseId");
                                });
                        });
                });

            modelBuilder.Entity("Escola.Dominio.Turmas.TurmaComDuracao", b =>
                {
                    b.OwnsOne("Escola.Dominio.Turmas.DuracaoTurma", "Duracao", b1 =>
                        {
                            b1.Property<long>("TurmaComDuracaoId")
                                .HasColumnType("bigint");

                            b1.Property<int?>("Quantidade")
                                .HasColumnName("DuracaoQuantidade")
                                .HasColumnType("int");

                            b1.Property<byte[]>("RowVersion")
                                .IsConcurrencyToken()
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnName("RowVersion")
                                .HasColumnType("rowversion");

                            b1.Property<string>("Tipo")
                                .IsRequired()
                                .HasColumnName("DuracaoTipo")
                                .HasColumnType("varchar(20)");

                            b1.HasKey("TurmaComDuracaoId");

                            b1.ToTable("Turmas");

                            b1.WithOwner()
                                .HasForeignKey("TurmaComDuracaoId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
