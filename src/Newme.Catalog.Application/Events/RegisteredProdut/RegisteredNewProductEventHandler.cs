using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Factories;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewProductEventHandler : INotificationHandler<RegisteredNewProductEvent>
    {
        private readonly ILogger<RegisteredNewProductEventHandler> _logger;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductConsultingModelFactory _factory;

        public RegisteredNewProductEventHandler(
            ILogger<RegisteredNewProductEventHandler> logger,
            IProductQueryRepository productQueryRepository,
            IProductConsultingModelFactory factory)
        {
            _logger = logger;
            _productQueryRepository = productQueryRepository;
            _factory = factory;
        }

        public async Task Handle(RegisteredNewProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(RegisteredNewProductEvent), DateTime.Now, notification.ToString());

            var consultingModel = await _factory.Build(notification.Product);

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db, data {data}.", 
                notification.Id, DateTime.Now, consultingModel.ToString());

            await _productQueryRepository.AddAsync(consultingModel)
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, consultingModel.Id, DateTime.Now, notification.Detail);
        }
    }
}