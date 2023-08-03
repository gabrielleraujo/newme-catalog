using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Events;
using Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Sent;
using Newme.Catalog.Domain.Messaging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class ReduceProductsStockCommandHandler : 
        CommandHandler<ReduceProductsStockCommandHandler>, 
        IRequestHandler<ReduceProductsStockCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;
        private readonly IMessageBusServer _messageBus;

        public ReduceProductsStockCommandHandler(
            IMediator mediator,
            ILogger<ReduceProductsStockCommandHandler> logger,
            IProductCommandRepository repositoryCommand,
            IMessageBusServer messageBus) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
            _messageBus = messageBus;
        }

        public async Task<ValidationResult> Handle(ReduceProductsStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ReduceProductsStockCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                
                _messageBus.Publish(
                    new ReducedProductsStockSentEvent(), 
                    ReducedProductsStockSentEvent.EventName);

                return command.ValidationResult;
            }

            var products = await _repositoryCommand.GetByAsync<Product>(x => 
                command.Event.Items
                    .Select(x => x.ProductId)
                    .Contains(x.Id.value));

            var sentEvent = new ReducedProductsStockSentEvent(
                purchaseId: command.Event.PurchaseId
            );
            
            await Parallel.ForEachAsync(
                source: products, 
                cancellationToken: cancellationToken, 
                body: async (product, stoppingToken) => await ReduceProductStock(
                    product, sentEvent, command)).ConfigureAwait(true);

            _messageBus.Publish(sentEvent, ReducedProductsStockSentEvent.EventName);

            _logger.LogInformation($"{nameof(ReduceProductsStockCommandHandler)} successfully completed");

            return ValidationResult;
        }

        private async Task<ValidationResult> ReduceProductStock(
            Product product, 
            ReducedProductsStockSentEvent sentEvent,
            ReduceProductsStockCommand command)
        {
            var eventItem = command.Event.Items.FirstOrDefault(x => x.ProductId == product.Id.value);

            var quantityAchieved = product.ReduceStock(eventItem!.Quantity);

            sentEvent.AddItem(new ReducedProductItemStockSentEvent(
                productId: eventItem!.ProductId,
                quantityAchieved: quantityAchieved
            ));

            _repositoryCommand.Update(product);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                if (product.Active == false)
                {
                    var deactivateProductCommand = new DeactivateProductCommand(
                        id: product.Id.value
                    );
                    await _mediator.Send(deactivateProductCommand);
                }

                var changeProductPriceEvent = new ChangedProductEvent(
                    product: product,
                    detail: $"reduced product stock to {product.Stock}."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            return ValidationResult;
        }
    }
}
