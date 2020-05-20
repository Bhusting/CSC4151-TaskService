using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSC4151_TaskService.Controllers
{
    [ApiController]
    [Route("Ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Ping()
        {
            return "Task Pong";
        }
    }
}
