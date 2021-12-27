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

        public async Task<bool> CreateEvent(Event newEvent)
        {
            try
            {
                var newEventResult = await _eventService.CreateEvent(newEvent);

                if (newEventResult.Payload.ValueKind != JsonValueKind.Null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<object> GetResult()
        {

            try
            {
                var savedData = await _eventService.GetLatestEvent();
                if (savedData == null)
                    return "No date present";

                var payloadWithData = JsonSerializer.Deserialize<Payload>(savedData.Payload.ToString());

                double minvalue = Double.MaxValue;
                List<Fixture.Core.Models.Market> winnersMark = new List<Fixture.Core.Models.Market>();
                foreach (var mark in payloadWithData.Markets)
                {
                    if (mark.Price < minvalue)
                    {
                        minvalue = mark.Price;
                        winnersMark.Clear();
                        winnersMark.Add(mark);
                    }
                    else if (mark.Price == minvalue)
                    {
                        winnersMark.Add(mark);
                    }

                }

                var result = new Event();
                result.Type = Enum.GetName(FuncationType.ResultFixture);
                result.Version = savedData.Version;
               
                return new { type = result.Type, payload = new { payloadWithData.Id, winners = winnersMark.Select(mar => new Winner() { Id = mar.Id }).ToList() } };
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }

        public async Task<bool> UpdateMarket(Event updateEevent)
        {
            try
            {
                if (updateEevent.Type != Enum.GetName(FuncationType.UpdateFixture))
                    return false;

                var updatePayload = JsonSerializer.Deserialize<Payload>(updateEevent.Payload.GetRawText());
                if (updatePayload.Markets != null)
                {
                    var savedData = await _eventService.GetEventById(updatePayload.Id);
                    await _eventService.UpdateEvent(updateEevent, savedData);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
