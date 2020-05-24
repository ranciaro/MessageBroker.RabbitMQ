using BrokerMessage.RabbitMQ.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BrokerMessage.RabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageBrokerController : ControllerBase
    {
        private readonly ICustomerPublisher _customerPublisher;

        public MessageBrokerController(ICustomerPublisher customerPublisher)
        {
            _customerPublisher = customerPublisher;
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            var customer = new Core.Messages.CustomerMessage
            {
                Content = $"Conteúdo {Guid.NewGuid().ToString()}",
                CreateDate = DateTime.Now
            };
            await _customerPublisher.Publish(customer);
            return Ok();
        }
    }
}