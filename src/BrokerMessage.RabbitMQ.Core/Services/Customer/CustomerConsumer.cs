using BrokerMessage.RabbitMQ.Core.Messages;
using Silverback.Messaging.Subscribers;
using System;

namespace BrokerMessage.RabbitMQ.Core.Services
{
    public class CustomerConsumer : ISubscriber
    {
        public CustomerReceivedOk OnMessageReceived(CustomerMessage message)
        {
            Console.WriteLine($"Message content: {message.Content}. Create Date: {message.CreateDate}");
            return new CustomerReceivedOk(DateTime.Now);
        }
    }
}