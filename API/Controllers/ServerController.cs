using System.Text.Json;
using api.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class ServerController : ControllerBase
    {
        private readonly IProcessHost processHost;
        private readonly IFileManager fileManager;

        public ServerController(IProcessHost processHost, IFileManager fileManager)
        {
            this.processHost = processHost;
            this.fileManager = fileManager;
        }        

        [HttpGet]
        [Route("GetServerStatus")]
        public ActionResult GetServerStatus()
        {
            return Ok(processHost.GetServerStatus());
        }

        [HttpGet]
        [Route("Start")]
        public ActionResult Start()
        {
            processHost.StartServer();

            return Ok();
        }

        [HttpGet]
        [Route("Stop")]
        public ActionResult Stop()
        {
            processHost.StopServer();

            return Ok();
        }

        [HttpGet]
        [Route("GetSettings")]
        public ActionResult GetSettings()
        {
            return Ok(fileManager.GetSettings());
        }

        [HttpGet]
        [Route("GetEvent")]
        public ActionResult GetEvent()
        {
            return Ok(fileManager.GetEvent());
        }

        [HttpGet]
        [Route("GetEventRules")]
        public ActionResult GetEventRules()
        {
            return Ok(fileManager.GetEventRules());
        }

        [HttpPost]
        [Route("SaveSettings")]
        public ActionResult SaveSettings([FromBody]JsonElement text)
        {
            fileManager.SaveSettings(JsonSerializer.Serialize(text,new JsonSerializerOptions(){WriteIndented = true}));
            return Ok();
        }

        [HttpPost]
        [Route("SaveEvent")]
        public ActionResult SaveEvent([FromBody]JsonElement text)
        {
            fileManager.SaveEvent(JsonSerializer.Serialize(text,new JsonSerializerOptions(){WriteIndented = true}));
            return Ok();
        }

        [HttpPost]
        [Route("SaveEventRules")]
        public ActionResult SaveEventRules([FromBody]JsonElement text)
        {
            fileManager.SaveEventRules(JsonSerializer.Serialize(text,new JsonSerializerOptions(){WriteIndented = true}));
            return Ok();
        }
    }
}