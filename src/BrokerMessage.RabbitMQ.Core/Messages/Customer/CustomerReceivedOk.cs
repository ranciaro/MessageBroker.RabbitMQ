using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerMessage.RabbitMQ.Core.Messages
{
    public class CustomerReceivedOk
    {
        public CustomerReceivedOk(DateTime processedDate)
        {
            ProcessedDate = processedDate;
        }
        public DateTime ProcessedDate { get; set; }
    }
}
