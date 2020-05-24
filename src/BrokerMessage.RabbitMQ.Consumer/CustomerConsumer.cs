using BrokerMessage.RabbitMQ.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokerMessage.RabbitMQ.Consumer
{
    public class CustomerConsumer : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            new Producer().Receive();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            new Producer().Receive();
            Console.WriteLine("executei ");
        }
    }
}
