using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SearchApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchApi.DbModel;
using System.Text;

using SearchApi.Interface;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json.Linq;

namespace SearchApi.RabbitMQ
{
    public class RabbitMQProducerOrder: IMessageProducer
    {
        private readonly OrderDbContext _context;

        public RabbitMQProducerOrder(IServiceScopeFactory factory)
        {
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
        }
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue");
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "demo-queue", body: body);
        }

      

        public void ReceiveMessageUser()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var _user = JsonConvert.DeserializeObject<User>(message);
                await _context.Users.AddAsync(_user);

            };
            channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
        }

        public void ReceiveMessageProduct()
        {

            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "demo-queue",
                       durable: true,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, e) =>
            {
                await Console.Out.WriteLineAsync("Пимал");
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                await Console.Out.WriteLineAsync(message);
                var product = JsonConvert.DeserializeObject<string>(message);
                var _product = JsonConvert.DeserializeObject<Product>(product);
                await _context.Products.AddAsync(_product);
                await _context.SaveChangesAsync();
            };
            channel.BasicConsume(queue: "demo-queue", autoAck: true, consumer: consumer);
        }

    }
}
