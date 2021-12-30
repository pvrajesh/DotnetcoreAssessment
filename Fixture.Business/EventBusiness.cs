using Fixture.Business.Utils;
using Fixture.Core.Models;
using Fixture.Core.Services;
using Fixture.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fixture.Business
{
    public class EventBusiness : IEventBusiness
    {
        private readonly IEventService _eventService;
        

        public EventBusiness(IEventService eventService)
        {
            this._eventService = eventService;

        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            try
            {
                
                var newEventResult = await _eventService.CreateEvent(newEvent);
                return newEventResult;
             }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<object> GetResult(int versionID)
        {

            try
            {

                var result = new ResponseEvent();
                result.Type = Enum.GetName(FixtureType.ResultFixture);
                //Checking if any data presents if no then sending back empty winner class
                var savedData = await _eventService.GetEventByversionIdAsync(versionID);
                if (savedData == null)
                {
                   
                    //Dynamically creating a winner object and retuning
                    result.Payload = new ResponsePayload { Winners = new List<Winner>() };
                    return result;
                }
                
                
                var payloadWithData = JsonSerializer.Deserialize<Payload>(savedData.Payload.ToString());


                // Initializing the minvalue to find the list of winners 
                double minValue = Double.MaxValue;

                // initializing the winners list, this for when multiple teams participate in the game like triangle series
                // or if both teams in same market value
                List<Fixture.Core.Models.Market> winnersMark = new List<Fixture.Core.Models.Market>();


                foreach (var mark in payloadWithData.Markets)
                {
                    // current min value is less than the price then
                    // updating minValue and clearing exsting winner
                    // list and then to adding current value to winner list                   
                    if (mark.Price < minValue)
                    {
                        minValue = mark.Price;
                        winnersMark.Clear();
                        winnersMark.Add(mark);
                    }
                    else if (mark.Price == minValue)
                    {
                        winnersMark.Add(mark);
                    }

                }

                               
                result.Version = savedData.Version;
                result.Payload = new ResponsePayload { Id = payloadWithData.Id, Winners = winnersMark.Select(mar => new Winner() { Id = mar.Id }).ToList() };
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }

        public async Task<bool> UpdateMarket(Event updateEvent)
        {
            try
            {
                if (updateEvent.Type != Enum.GetName(FixtureType.UpdateFixture))
                    return false;

                var updatePayload = JsonSerializer.Deserialize<Payload>(updateEvent.Payload.GetRawText());
                if (updatePayload.Markets != null)
                {
                    var savedData = await _eventService.GetEventById(updateEvent.Version, updatePayload.Id);
                    await _eventService.UpdateEventMarket(updateEvent, savedData);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
