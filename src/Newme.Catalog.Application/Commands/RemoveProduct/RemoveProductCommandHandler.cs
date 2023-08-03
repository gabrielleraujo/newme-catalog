using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RemoveProductCommandHandler : 
        CommandHandler<RemoveProductCommandHandler>, 
        IRequestHandler<RemoveProductCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RemoveProductCommandHandler(
            IMediator mediator,
            ILogger<RemoveProductCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RemoveProductCommandHandler)} starting");

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

            _repositoryCommand.Delete(product.Id);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new RemoveProductEvent(
                    productId: product.Id,
                    detail: "product removed."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(FixProductNameAndDescriptionCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}