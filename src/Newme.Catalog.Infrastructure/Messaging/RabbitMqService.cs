using System.Text;
using Microsoft.Extensions.Configuration;
using Newme.Catalog.Domain.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Newme.Catalog.Infrastructure.Messaging
{
    public class RabbitMqService : IMessageBusServer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string _exchange = "catalog-service";

        public RabbitMqService(IConfiguration configuration)
        {
            var connectionFactory = new ConnectionFactory {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("catalog-service-publisher");

            _channel = _connection.CreateModel();
        }

        public void Publish(object data, string routingKey)
        {
            var type = data.GetType();

            var payload = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payload);

            Console.WriteLine($"{type.Name} Published");
            
            _channel.BasicPublish(_exchange, routingKey, null, byteArray);
        }
    }
}