using System.Collections.Generic;
using System.Threading.Tasks;
using Fixture.Core.Models;

namespace Fixture.Core.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllWithMetaData();
        Task<Event> GetEventById(int id);
        Task<Event> GetLatestEvent();
        Task<Event> CreateEvent(Event newEvent);
        Task UpdateEvent(Event eventToBeUpdated, Event existingEvent);
      
    }
}