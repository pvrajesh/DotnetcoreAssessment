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

       

        /// <summary>
        /// Get all events data includes meta data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Event>> GetAllWithMetaData()
        {
            return await _unitOfWork.Events
                .GetAllWithMetadataAsync();
        }

        /// <summary>
        /// get event by version id and payload ID
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="payloadId"></param>
        /// <returns></returns>
        public async Task<Event> GetEventById(int versionID, int payloadId)
        {
            return await _unitOfWork.Events.GetEventByIdAsync(versionID, payloadId);
                
        }

        public async Task<Event> GetEventByversionIdAsync(int versionID)
        {
            return await _unitOfWork.Events.GetEventByversionIdAsync(versionID);

        }

        /// <summary>
        /// Get the latest event based on payload ID
        /// </summary>
        /// <returns></returns>
        public async Task<Event> GetLatestEvent()
        {
            return await _unitOfWork.Events.GetLatestEvent();

        }

        /// <summary>
        /// Update the event markets based on exsting data 
        /// </summary>
        /// <param name="eventToBeUpdated"></param>
        /// <param name="existingEvent"></param>
        /// <returns></returns>
        public async Task UpdateEventMarket(Event eventToBeUpdated, Event existingEvent)
        {

           var exstingPayload= await Fixture.Core.Utils.DataConversion.JsonToEntity<Payload>(existingEvent.Payload);
            var newPayload = await Fixture.Core.Utils.DataConversion.JsonToEntity<Payload>(eventToBeUpdated.Payload);

            if(exstingPayload.Id == newPayload.Id)
            {
               
                foreach (var item in newPayload.Markets)
                {
                   int itemFound= exstingPayload.Markets.FindIndex(mk => mk.Id == item.Id);

                    if (itemFound >= 0)
                    {
                        exstingPayload.Markets[itemFound] = item;
                    }
                }
                
                existingEvent.Type = eventToBeUpdated.Type;
            }
            
            await _unitOfWork.CommitAsync();
        }
    }
}