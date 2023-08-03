using MediatR;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewCategoryEventHandler : INotificationHandler<RegisteredNewCategoryEvent>
    {
        private readonly ILogger<RegisteredNewCategoryEventHandler> _logger;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public RegisteredNewCategoryEventHandler(
            ILogger<RegisteredNewCategoryEventHandler> logger,
            ICategoryQueryRepository categoryQueryRepository)
        {
            _logger = logger;
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task Handle(RegisteredNewCategoryEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received Event {event} - id: {id} at: {date}, data {data}.", 
                notification.Id, nameof(RegisteredNewCategoryEvent), DateTime.Now, notification.ToString());

            var consultingModel = CategoryConsultingModel.MapFromDomain(notification.Category);

            _logger.LogInformation("Event id: {id} start running eventual consistence at {date}, saved data on consulting db, data {data}.", 
                notification.Id, DateTime.Now, consultingModel.ToString());

            await _categoryQueryRepository.AddAsync(CategoryConsultingModel.MapFromDomain(notification.Category))
                .ConfigureAwait(false);

            _logger.LogInformation("Event id: {id} is finish, category id: {categoryId} at: {date}\n detail: {detail}", 
                notification.Id, consultingModel.Id, DateTime.Now, notification.Detail);
        }
    }
}