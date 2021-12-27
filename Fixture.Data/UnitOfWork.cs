using System.Threading.Tasks;
using Fixture.Core;
using Fixture.Core.Repositories;
using Fixture.Data.Repositories;

namespace Fixture.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventDbContext _context;
        private EventRepository _eventRepository;
        

        public UnitOfWork(EventDbContext context)
        {
            this._context = context;
        }

       

        public IEventRepository Events => _eventRepository = _eventRepository ?? new EventRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}