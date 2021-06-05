using BuisnessLayer.AccessLayer.IAccessLayer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BuisnessLayer.AccessLayer
{
    public class SubsciberBreakfast : ISubsciber
    {
        public void GetBreakfastSubscriber()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var ExchangeName = "OrderTestTopicExchange";
                var QueueBreakfast = "BreakfastQueue";
                var routingKey = "food.breakfast.*";

                //Declare an Exchange
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

                channel.QueueDeclare(queue: QueueBreakfast,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                //Bind the Exchange with Diffrent queue
                channel.QueueBind(QueueBreakfast, ExchangeName, routingKey);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: QueueBreakfast, autoAck: true, consumer: consumer);

            }
        }
    }
}
