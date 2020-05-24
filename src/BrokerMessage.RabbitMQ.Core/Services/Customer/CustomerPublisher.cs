using BrokerMessage.RabbitMQ.Core.Messages;
using Silverback.Messaging.Publishing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerMessage.RabbitMQ.Core.Services
{
    public class CustomerPublisher : ICustomerPublisher
    {
        private readonly IPublisher _publisher;
        public CustomerPublisher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Publish(CustomerMessage customerMessage)
        {
            await _publisher.PublishAsync(customerMessage);
            //Console.WriteLine($"Mensagem foi entregue. Retorno: {result.Single().ProcessedDate}");
        }
    }
}
