using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewColorEventHandler : INotificationHandler<RegisteredNewColorEvent>
    {
        private readonly ILogger<RegisteredNewColorEventHandler> _logger;
        private readonly IColorQueryRepository _colorQueryRepository;

        public RegisteredNewColorEventHandler(
            ILogger<RegisteredNewColorEventHandler> logger,
            IColorQueryRepository colorQueryRepository)
        {
            _logger = logger;
            _colorQueryRepository = colorQueryRepository;
        }

        public async Task Handle(RegisteredNewColorEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(RegisteredNewProductEvent), DateTime.Now, notification.ToString());

            var consultingModel = ColorConsultingModel.MapFromDomain(notification.Color);

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db, data {data}.", 
                notification.Id, DateTime.Now, consultingModel.ToString());

            await _colorQueryRepository.AddAsync(ColorConsultingModel.MapFromDomain(notification.Color))
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, consultingModel.Id, DateTime.Now, notification.Detail);
        }
    }
}