using System;
using System.Threading;
using System.Threading.Tasks;

namespace Escola.Dominio.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
