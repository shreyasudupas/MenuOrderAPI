using Newtonsoft.Json;
using OrderAPI.Models;
using RabbitMQ.Client;
using System.Text;

namespace BuisnessLayer
{
    public class Producer : IProducer
    {

        public void TopicExchangeQueue(Order orderItems)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchangeName = "OrderTestTopicExchange";
                //Declare an Exchange
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);


                var routingKey = "food.breakfast.*";
                var message = JsonConvert.SerializeObject(orderItems);

                //Encode the message in Bytes
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
            }
        }
    }
}
