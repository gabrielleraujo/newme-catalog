using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Events;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Domain.Messaging;
using Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Sent;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class DeactivateProductCommandHandler : 
        CommandHandler<DeactivateProductCommandHandler>, 
        IRequestHandler<DeactivateProductCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;
        private readonly IMessageBusServer _messageBus;

        public DeactivateProductCommandHandler(
            IMediator mediator,
            ILogger<DeactivateProductCommandHandler> logger,
            IProductCommandRepository repositoryCommand,
            IMessageBusServer messageBus) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
            _messageBus = messageBus;
        }

        public async Task<ValidationResult> Handle(DeactivateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeactivateProductCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var product = await _repositoryCommand.FindByAsync<Product>(x => x.Id.value == command.Id);

            if(product == null)
            {
                AddError("Product not found");
                _messageBus.Publish(new DeactivatedProductSentEvent(), DeactivatedProductSentEvent.EventName);
                return ValidationResult;
            }
            
            product.Deactivate();

            _repositoryCommand.Update(product);            
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var deactivatedProductEvent = new DeactivatedProductEvent(
                    productId: product.Id,
                    detail: "deactivated product."
                );
                await _mediator.Publish(deactivatedProductEvent).ConfigureAwait(false);

                var sentEvent = new DeactivatedProductSentEvent(
                    productId: product.Id.value
                );

                _messageBus.Publish(sentEvent, DeactivatedProductSentEvent.EventName);
            }

            _logger.LogInformation($"{nameof(DeactivateProductCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}