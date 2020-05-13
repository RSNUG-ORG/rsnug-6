using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Escola.Dominio.Turmas;

namespace Escola.Infra.EF.Repositorios
{
    public sealed class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly EscolaContextoEF _contexto;

        public TurmasRepositorio(EscolaContextoEF contexto)
        {
            this._contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<TurmaBase> AdicionarAsync(TurmaBase turma)
        {
            return (await _contexto.Turmas.AddAsync(turma)).Entity;
        }

        public async Task<Maybe<TurmaBase>> RecuperarAsync(long id)
            => await _contexto.Turmas.FirstOrDefaultAsync(c => c.Id == id);
    }
}
