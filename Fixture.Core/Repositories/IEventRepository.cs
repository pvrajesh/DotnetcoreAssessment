using System.Collections.Generic;
using System.Threading.Tasks;
using Fixture.Core.Models;

namespace Fixture.Core.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllWithMetadataAsync();
        Task<Event> GetEventByIdAsync(int versionId, int payLoadId);
        Task<Event> GetEventByversionIdAsync(int versionId);

        Task<Event> GetLatestEvent();

    }
}