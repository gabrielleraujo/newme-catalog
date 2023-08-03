using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newme.Catalog.Application.Commands;
using MediatR;
using Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Received;
using Newme.Catalog.Domain.Messaging;

namespace Newme.Catalog.Application.Subscribers
{
    public class PurchaseOrderCreatedSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string Queue = "catalog-service/purchase-order-payment-authorized";
        private const string RoutingKeySubscribe = "purchase-order-payment-authorized";
        private readonly IServiceProvider _serviceProvider;
        private const string TrackingsExchange = "purchase-service";
 
        public PurchaseOrderCreatedSubscriber(IServiceProvider serviceProvider)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
 
            _connection = connectionFactory.CreateConnection("purchase-order-payment-authorized-consumer");
 
            _channel = _connection.CreateModel();
 
            _channel.QueueDeclare(
                queue: Queue,
                durable: true,
                exclusive: false,
                autoDelete: false);
 
            _channel.QueueBind(Queue, TrackingsExchange, RoutingKeySubscribe);
 
            _serviceProvider = serviceProvider;
        }
 
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
 
            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var @event = JsonConvert.DeserializeObject<CreatedPurchaseOrderReceivedEvent>(contentString);
 
                Console.WriteLine($"Message in catalog, purchase-order-payment-authorized external event is received with purchase id: {@event!.PurchaseId}");
 
                Complete(@event, stoppingToken).Wait();
 
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
 
            _channel.BasicConsume(Queue, false, consumer);
 
            return Task.CompletedTask;
        }

        public async Task Complete(CreatedPurchaseOrderReceivedEvent @event, CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBusServer>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new ReduceProductsStockCommand(@event);
            var response = await mediator.Send(command);
        }
    }
}
