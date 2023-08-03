namespace Newme.Catalog.Domain.Messaging
{
    public interface IMessageBusServer
    {
        void Publish(object data, string routingKey);
    }
}