using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class ChangeProductPriceCommandHandler : 
        CommandHandler<ChangeProductPriceCommandHandler>, 
        IRequestHandler<ChangeProductPriceCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public ChangeProductPriceCommandHandler(
            IMediator mediator,
            ILogger<ChangeProductPriceCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(ChangeProductPriceCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ChangeProductPriceCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var product = await _repositoryCommand.FindByAsync<Product>(x => x.Id.value == command.Id);

            if (product == null)
            {
                AddError("Product not found");
                return ValidationResult;
            }

            product.ChangePrice(new Money(command.Currency, command.Amount));

            _repositoryCommand.Update(product);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new ChangedProductEvent(
                    product: product,
                    detail: "Changed product price."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(ChangeProductPriceCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
