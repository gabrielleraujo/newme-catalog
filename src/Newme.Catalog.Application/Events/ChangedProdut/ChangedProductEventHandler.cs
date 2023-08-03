using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Factories;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class ChangedProductEventHandler : INotificationHandler<ChangedProductEvent>
    {
        private readonly ILogger<ChangedProductEventHandler> _logger;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductConsultingModelFactory _factory;

        public ChangedProductEventHandler(
            ILogger<ChangedProductEventHandler> logger,
            IProductQueryRepository productQueryRepository,
            IProductConsultingModelFactory factory)
        {
            _logger = logger;
            _productQueryRepository = productQueryRepository;
            _factory = factory;
        }

        public async Task Handle(ChangedProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(ChangedProductEvent), DateTime.Now, notification.ToString());

            var consultingModel = await _factory.Build(notification.Product);

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db, data {data}.", 
                notification.Id, DateTime.Now, consultingModel.ToString());

            await _productQueryRepository.UpdateManyAsync(notification.Product.Id.value, consultingModel)
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, consultingModel.Id, DateTime.Now, notification.Detail);
        }
    }
}