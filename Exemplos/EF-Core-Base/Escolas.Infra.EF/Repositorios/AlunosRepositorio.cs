using CSharpFunctionalExtensions;
using Escola.Dominio.Alunos;
using Escola.Dominio.Shared;
using Escola.Infra.EF;
using Escola.Dominio.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Escola.Infra.EF.Repositorios
{
    public sealed class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly EscolaContextoEF _contexto;

        public AlunosRepositorio(EscolaContextoEF contexto)
        {
            this._contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<Aluno> AdicionarAsync(Aluno aluno)
        {
            return (await _contexto.Alunos.AddAsync(aluno)).Entity;
        }

        public async Task<Maybe<Aluno>> RecuperarAsync(long id)
            => await _contexto.Alunos.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Maybe<Aluno>> RecuperarPorEmailAsync(Email email)
            => await _contexto.Alunos.FirstOrDefaultAsync(c=> c.Email == email);
    }
}
