﻿using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace Escola.Dominio.Turmas
{
    public interface ITurmasRepositorio
    {
        Task<TurmaBase> AdicionarAsync(TurmaBase turma);
        Task<Maybe<TurmaBase>> RecuperarAsync(long id);
    }
}
