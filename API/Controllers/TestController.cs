using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class TestController : ControllerBase
    {
        private IProcessHost processHost;

        public TestController(IProcessHost processHost)
        {
            this.processHost = processHost;
        }

        [HttpGet]
        public ActionResult Get()
        {
            processHost.StartServer();

            return Ok();
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
        [Route("GetProcesses")]
        public ActionResult GetProcesses()
        {
            return Ok(processHost.GetProcesses());
            //return Ok("THIS IS A TEST");
        }

        [HttpGet]
        [Route("IsServerRunning")]
        public ActionResult IsServerRunning()
        {
            return Ok(processHost.IsServerRunning());
        }
    }
}