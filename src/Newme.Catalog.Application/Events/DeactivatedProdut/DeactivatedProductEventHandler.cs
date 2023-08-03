using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class DeactivatedProductEventHandler : INotificationHandler<DeactivatedProductEvent>
    {
        private readonly ILogger<DeactivatedProductEventHandler> _logger;
        private readonly IProductQueryRepository _productQueryRepository;

        public DeactivatedProductEventHandler(
            ILogger<DeactivatedProductEventHandler> logger,
            IProductQueryRepository productQueryRepository)
        {
            _logger = logger;
            _productQueryRepository = productQueryRepository;
        }

        public async Task Handle(DeactivatedProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(DeactivatedProductEvent), DateTime.Now, notification.ToString());

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, deactivating product on consulting db.", 
                notification.Id, DateTime.Now);

            await _productQueryRepository.DeactivateAsync(notification.ProductId.value)
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, notification.ProductId.value, DateTime.Now, notification.Detail);
        }
    }
}