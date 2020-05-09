using Escola.Dominio.Alunos;
using Escola.Infra.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Escolas.Infra.EF.Repositorios
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
    }
}
