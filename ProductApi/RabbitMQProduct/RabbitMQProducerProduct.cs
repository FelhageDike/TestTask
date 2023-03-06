using Newtonsoft.Json;
using ProductApi.Interface;
using RabbitMQ.Client;
using System.Text;

namespace ProductApi.RabbitMQProduct
{
    public class RabbitMQProducerProduct : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "demo-queue",
                       durable: true,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "demo-queue", body: body);
        }
    }
}
