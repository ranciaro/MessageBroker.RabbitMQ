using BrokerMessage.RabbitMQ.Core;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMessage.RabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageBrokerController : ControllerBase
    {
        [HttpGet]
        public IActionResult SendMessage()
        {            
            new Producer().Send();
            //new Producer().Receive();
            return Ok();
        }
    }
}