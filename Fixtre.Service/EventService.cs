using System.Collections.Generic;
using System.Threading.Tasks;
using Fixture.Core;
using Fixture.Core.Models;
using Fixture.Core.Services;

namespace Fixture.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            await _unitOfWork.Events.AddAsync(newEvent);
            await _unitOfWork.CommitAsync();
            return newEvent;
        }

       

        public async Task<IEnumerable<Event>> GetAllWithMetaData()
        {
            return await _unitOfWork.Events
                .GetAllWithMetadataAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _unitOfWork.Events.GetEventByIdAsync(id);
                
        }

        public async Task<Event> GetLatestEvent()
        {
            return await _unitOfWork.Events.GetLatestEvent();

        }

        public async Task UpdateEvent(Event eventToBeUpdated, Event existingEvent)
        {

           var exstingPayload= await Fixture.Core.Utils.DataConversion.JsonToEntity<Payload>(existingEvent.Payload);
            var newPayload = await Fixture.Core.Utils.DataConversion.JsonToEntity<Payload>(eventToBeUpdated.Payload);

            if(exstingPayload.Id == newPayload.Id)
            {
                exstingPayload.Markets = newPayload.Markets;
                existingEvent.Type = eventToBeUpdated.Type;
            }
            
           

            await _unitOfWork.CommitAsync();
        }
    }
}