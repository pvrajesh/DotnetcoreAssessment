using System.Collections.Generic;
using System.Threading.Tasks;
using Fixture.Core.Models;

namespace Fixture.Core.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllWithMetaData();
        Task<Event> GetEventById(int versionID,int payloadId);
        Task<Event> GetEventByversionIdAsync(int vesionId);
        Task<Event> GetLatestEvent();
        Task<Event> CreateEvent(Event newEvent);
        Task UpdateEventMarket(Event eventToBeUpdated, Event existingEvent);
      
    }
}