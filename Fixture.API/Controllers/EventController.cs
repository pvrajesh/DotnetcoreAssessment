using Fixture.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Fixture.Core.Models;
using Fixture.Core.Repositories;
using statuscodes= Microsoft.AspNetCore.Http.StatusCodes;
using Fixture.Data;
using Fixture.Business;

namespace Fixture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventBusiness _eventBusiness;

        public EventController( IEventBusiness eventBusiness)
        {
            //this._eventService = eventService;
            this._eventBusiness = eventBusiness;
            
        }

        // GET: api/<Fixture>
        [HttpGet]
        public async Task<ActionResult> GetResult()        
       {

            try
            {
                return Ok(await _eventBusiness.GetResult());

            }
            catch (IOException iox)
            {

                return StatusCode(statuscodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return StatusCode(statuscodes.Status400BadRequest);

            }


        }

        
        // POST api/<Fixture>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Event eventPayload)
        {
            try
            {
                var newEvent = await _eventBusiness.CreateEvent(eventPayload);


                return Ok(statuscodes.Status200OK);
            }
            catch (IOException iox)
            {

                return StatusCode(statuscodes.Status500InternalServerError);
            }
            catch(Exception ex)
            {
                return StatusCode(statuscodes.Status400BadRequest);

            }
        }

        // PUT api/<Fixture>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Event value)
        {
            try
            {
                if (_eventBusiness.UpdateMarket(value).Result)
                    return Ok(statuscodes.Status200OK);
                else
                    return StatusCode(statuscodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {

                return StatusCode(statuscodes.Status500InternalServerError);

            }

        }
       
    }
}
