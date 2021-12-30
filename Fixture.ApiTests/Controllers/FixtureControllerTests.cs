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
using Fixture.Business.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fixture.Controllers.Tests
{
    [TestClass]
    public class FixtureControllerTests
    {
        private  FixtureController _controller;
        
        readonly IServiceProvider _services =
        Program.CreateWebHostBuilder(new string[] { }).Build().Services;
        private string payloadPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + "\\Payloads";

        [TestInitialize]
        public void FixtureControllerTest()
        {

            var eventBusiness = _services.GetRequiredService<IEventBusiness>();
            _controller = new FixtureController(eventBusiness);

            
        }

        
        [TestMethod]
        public async Task PostTest()
        {
            var createPayload = payloadPath+"\\01_create_fixture.json";
           
            var createInput = JsonSerializer.Deserialize<Event>(File.ReadAllText(createPayload));
            var returnedActionResult = await _controller.Post(createInput);

            var okResult = returnedActionResult as OkObjectResult;
            var resultEvent = okResult.Value as Event;

            Assert.AreEqual(resultEvent, createInput);
        }

        [TestMethod]
        public async Task PutTest()
        {
            var updatePayload = payloadPath + "\\02_update_price.json";
            var updateInput = JsonSerializer.Deserialize<Event>(File.ReadAllText(updatePayload));

            var returnedActionResult = await _controller.Put(updateInput);       

            var statusCodeResult = (IStatusCodeActionResult)returnedActionResult;

            Assert.IsTrue(statusCodeResult.StatusCode == 200);
        }


        [TestMethod]
        public async Task Result()
        {
            var actionResult = await _controller.GetResult(1);

            var okResult = actionResult as OkObjectResult;
            var resultEvent = okResult.Value as ResponseEvent;

            var resultPayload = payloadPath+"\\03_result_fixture.json";
            var expectedEvent = JsonSerializer.Deserialize<ResponseEvent>(File.ReadAllText(resultPayload));

            Assert.AreEqual(expectedEvent.Payload.Winners[0].Id, resultEvent.Payload.Winners[0].Id);

        }



        [TestMethod]
        public async Task Update_Fail()
        {

            var updatePayload = payloadPath+ "\\02_update_price.json";
            var updateInput = JsonSerializer.Deserialize<Event>(File.ReadAllText(updatePayload));

            //changing the type to other than update event
            updateInput.Type = Enum.GetName(FixtureType.CreateFixture);
            var returnedActionResult = await _controller.Put(updateInput);

            var statusCodeResult = (IStatusCodeActionResult)returnedActionResult;            
            Assert.IsTrue(statusCodeResult.StatusCode==400);   
          

        }

    }
}