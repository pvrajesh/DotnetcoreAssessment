using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fixture.Business;
using System.Text.Json;
using Fixture.Core.Models;
using System.Threading.Tasks;
using System.IO;
using System;
using Fixture.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Fixture.Controllers.Tests
{
    [TestClass]
    public class FixtureControllerTests
    {
        private  FixtureController _controller;
        
        readonly IServiceProvider _services =
        Program.CreateWebHostBuilder(new string[] { }).Build().Services;
        private string payloadPath = System.IO.Directory.GetCurrentDirectory() + "\\Payloads";

        [TestInitialize]
        public void FixtureControllerTest()
        {

            var eventBusiness = _services.GetRequiredService<IEventBusiness>();
            _controller = new FixtureController(eventBusiness);

            
        }

        
        [TestMethod]
        public async Task PostTest()
        {
            var createPayload = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\Payloads\\01_create_fixture.json";
           
            var createInput = JsonSerializer.Deserialize<Event>(File.ReadAllText(createPayload));
            var returnedActionResult = await _controller.Post(createInput);

            var okResult = returnedActionResult as OkObjectResult;
            var resultEvent = okResult.Value as Event;

            Assert.AreEqual(resultEvent, createInput);
        }

        [TestMethod]
        public async Task PutTest()
        {
            var updatePayload = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\Payloads\\02_update_price.json";

            var updateInput = JsonSerializer.Deserialize<Event>(File.ReadAllText(updatePayload));
            var returnedActionResult = await _controller.Put(updateInput);

            var okResult = returnedActionResult as OkObjectResult;
          

            Assert.IsTrue(okResult.StatusCode == 200);
        }


        [TestMethod]
        public async Task Result()
        {

            var actionResult = await _controller.GetResult(1);

            var okResult = actionResult as OkObjectResult;
            var resultEvent = okResult.Value as ResponseEvent;

            var resultPayload = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\Payloads\\03_result_fixture.json";

            var  expectedEvent = JsonSerializer.Deserialize<ResponseEvent>(File.ReadAllText(resultPayload));
          
            Assert.AreEqual(expectedEvent.Payload.Winners[0].Id, resultEvent.Payload.Winners[0].Id);

        }
    }
}