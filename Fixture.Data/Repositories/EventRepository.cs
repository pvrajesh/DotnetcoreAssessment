using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fixture.Core.Models;
using Fixture.Core.Utils;
using Fixture.Core.Repositories;

namespace Fixture.Data.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(EventDbContext context) 
            : base(context)
        { }

        public async Task<IEnumerable<Event>> GetAllWithMetadataAsync()
        {
            return await eventDbContext.Events
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            
            return await eventDbContext.Events               
                .SingleOrDefaultAsync(m => DataConversion.JsonToEntity<Payload>(m.Payload).Id == id);
        }

        public async Task<Event> GetLatestEvent()
        {
            return await eventDbContext.Events.OrderByDescending(m => DataConversion.JsonToEntity<Payload>(m.Payload).Id).SingleOrDefaultAsync();
                
        }

        private EventDbContext eventDbContext
        {
            get { return Context as EventDbContext; }
        }
    }
}