using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Fixture.Core.Models;
using statuscodes = Microsoft.AspNetCore.Http.StatusCodes;
using Fixture.Business;
using Fixture.Business.Utils;
using Fixture.Api;

namespace Fixture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixtureController : ControllerBase
    {

        private readonly IEventBusiness _eventBusiness;

        

        public FixtureController( IEventBusiness eventBusiness)
        {
             this._eventBusiness = eventBusiness;

           
        }
               

        // GET: api/<Fixture>
        [HttpGet]
        public async Task<ActionResult> GetResult(int versionID)        
       {
            try
            {
                return Ok(await _eventBusiness.GetResult(versionID));

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
                //validating the model and send back if any errors occured 
                if (ModelState.IsValid && eventPayload.Payload.ValueKind != JsonValueKind.Undefined)
                {

                    if (eventPayload.Type != Enum.GetName(FixtureType.CreateFixture))
                        return StatusCode(statuscodes.Status400BadRequest);

                    //calling business layer to create event with payload
                    return Ok(await _eventBusiness.CreateEvent(eventPayload));
                    
                }
                else
                    return ValidationProblem(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(statuscodes.Status400BadRequest);

            }
        }

        // PUT api/<Fixture>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Event updateEvent)
        {
            try
            {
             
               //validating the model and send backif any errors occured 
                if (ModelState.IsValid && updateEvent.Payload.ValueKind != JsonValueKind.Undefined)
                {
                    if (updateEvent.Type != Enum.GetName(FixtureType.UpdateFixture))
                        return BadRequest();

                    if (await _eventBusiness.UpdateMarket(updateEvent))
                        return Ok();
                    else
                        return UnprocessableEntity();


                }
                else
                    return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {

                return StatusCode(statusCode: statuscodes.Status500InternalServerError); 

            }

        }
       
    }
}
