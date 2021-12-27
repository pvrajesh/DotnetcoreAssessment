using System;
using System.Threading.Tasks;
using Fixture.Core.Repositories;

namespace Fixture.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }       
        Task<int> CommitAsync();
    }
}