using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using statuscodes= Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fixture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        // GET: api/<Fixture>
        [HttpGet]
        public string GetResult()
        {
            var rawdatasaved = JsonSerializer.Deserialize<Models.Event>(System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\jsonData.json"));
                    
            double minvalue=Double.MaxValue;
            List<Models.Market> winnersMark = new List<Models.Market>();
            foreach(var mark in rawdatasaved.Payload.Markets)
            {
                if(mark.Price < minvalue)
                {
                    minvalue = mark.Price;
                    winnersMark.Clear();
                    winnersMark.Add(mark);
                }
                else if(mark.Price == minvalue)
                {
                    winnersMark.Add(mark);
                }
                
            }

            var result = new Models.Event();
            result.Type = "ResultFixture";
            result.Version = rawdatasaved.Version;
            result.Payload = new Models.Payload();
            result.Payload.Id = rawdatasaved.Payload.Id;
            result.Payload.Winners = winnersMark.Select(mar => new Models.Winner() { Id = mar.Id }).ToList();
            
            return JsonSerializer.Serialize(result);
        }

        
        // POST api/<Fixture>
        [HttpPost]
        public IActionResult Post([FromBody] Models.Event value)
        {
            try
            {
                System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\jsonData.json", JsonSerializer.Serialize(value));
                return Ok(statuscodes.Status201Created);
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
        public IActionResult Put([FromBody] JsonElement value)
        {
            try
            {
                var updatedata = JsonSerializer.Deserialize<Models.Event>(value.GetRawText());
                if (updatedata.Payload.Markets != null)
                {
                    var savedData = JsonSerializer.Deserialize<Models.Event>(System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\jsonData.json"));

                    if (savedData.Payload.Id == updatedata.Payload.Id)
                    {
                        savedData.Payload.Markets = updatedata.Payload.Markets;
                    }

                    
                    System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\jsonData.json", JsonSerializer.Serialize(savedData));
                    return Ok();
                }
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
