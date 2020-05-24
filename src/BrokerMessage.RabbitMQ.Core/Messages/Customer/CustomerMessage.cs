using Silverback.Messaging.Messages;
using System;

namespace BrokerMessage.RabbitMQ.Core.Messages
{
    public class CustomerMessage : IMessage
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
