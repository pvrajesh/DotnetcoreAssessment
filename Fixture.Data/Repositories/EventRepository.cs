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

        public async Task<Event> GetEventByIdAsync(int versionId,int payLoadId)
        {
            
            return await eventDbContext.Events               
                .SingleOrDefaultAsync(m =>m.Version == versionId && DataConversion.JsonToEntity<Payload>(m.Payload).GetAwaiter().GetResult().Id  == payLoadId);
        }

        public async Task<Event> GetEventByversionIdAsync(int versionId)
        {

            return await eventDbContext.Events
                .SingleOrDefaultAsync(m => m.Version == versionId);
        }

        public async Task<Event> GetLatestEvent()
        {
            return await eventDbContext.Events.OrderByDescending(m => m.Version).SingleOrDefaultAsync();
                
        }

        private EventDbContext eventDbContext
        {
            get { return Context as EventDbContext; }
        }
    }
}