using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewSizeEventHandler : INotificationHandler<RegisteredNewSizeEvent>
    {
        private readonly ILogger<RegisteredNewSizeEventHandler> _logger;
        private readonly ISizeQueryRepository _sizeQueryRepository;

        public RegisteredNewSizeEventHandler(
            ILogger<RegisteredNewSizeEventHandler> logger,
            ISizeQueryRepository sizeQueryRepository)
        {
            _logger = logger;
            _sizeQueryRepository = sizeQueryRepository;
        }

        public async Task Handle(RegisteredNewSizeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(RegisteredNewSizeEvent), DateTime.Now, notification.ToString());

            var consultingModel = SizeConsultingModel.MapFromDomain(notification.Size);

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db, data {data}.", 
                notification.Id, DateTime.Now, consultingModel.ToString());

            await _sizeQueryRepository.AddAsync(SizeConsultingModel.MapFromDomain(notification.Size))
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, product id: {productId} at: {date}\n detail: {detail}", 
                notification.Id, consultingModel.Id, DateTime.Now, notification.Detail);
        }
    }
}