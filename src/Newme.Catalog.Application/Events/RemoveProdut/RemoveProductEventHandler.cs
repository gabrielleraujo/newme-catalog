using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class RemoveProductEventHandler : INotificationHandler<RemoveProductEvent>
    {
        private readonly ILogger<RemoveProductEventHandler> _logger;
        private readonly IProductQueryRepository _productQueryRepository;

        public RemoveProductEventHandler(
            ILogger<RemoveProductEventHandler> logger,
            IProductQueryRepository productQueryRepository)
        {
            _logger = logger;
            _productQueryRepository = productQueryRepository;
        }

        public async Task Handle(RemoveProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(RemoveProductEvent), DateTime.Now, notification.ToString());

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db.", 
                notification.Id, DateTime.Now);

            await _productQueryRepository.DeleteAsync(notification.ProductId.value)
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, notification.ProductId, DateTime.Now, notification.Detail);
        }
    }
}