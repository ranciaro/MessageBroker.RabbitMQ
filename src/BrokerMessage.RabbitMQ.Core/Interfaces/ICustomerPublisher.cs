using BrokerMessage.RabbitMQ.Core.Messages;
using Silverback.Messaging.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMessage.RabbitMQ.Core
{
    public interface ICustomerPublisher
    {
        Task Publish(CustomerMessage customerMessage);
    }
}
