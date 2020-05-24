using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BrokerMessage.RabbitMQ.Core
{
    public class Producer
    {
        private const string Queue = "Test";
        private const string Exchange = "amq.direct";
        private const string RoutingKey = "hello";

        private ConnectionFactory GetConnection() => new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672
        };
        public void Send()
        {
            var factory = GetConnection();

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(Exchange, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(Queue, false, false, false, null);
            channel.QueueBind(Queue, Exchange, RoutingKey, null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "amq.direct",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

        public void Receive()
        {
            var factory = GetConnection();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(Queue, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "Test",
                                 autoAck: true,
                                 consumer: consumer);
            Console.ReadKey();
        }
    }
}