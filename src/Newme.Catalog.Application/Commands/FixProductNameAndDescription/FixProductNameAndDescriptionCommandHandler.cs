using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Events;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class FixProductNameAndDescriptionCommandHandler : 
        CommandHandler<FixProductNameAndDescriptionCommandHandler>, 
        IRequestHandler<FixProductNameAndDescriptionCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public FixProductNameAndDescriptionCommandHandler(
            IMediator mediator,
            ILogger<FixProductNameAndDescriptionCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(FixProductNameAndDescriptionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(FixProductNameAndDescriptionCommandHandler)} starting");

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

            product
                .FixName(command.Name)
                .FixDescription(command.Description);

            _repositoryCommand.Update(product);        
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new ChangedProductEvent(
                    product: product,
                    detail: "fixed product name and and description."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(FixProductNameAndDescriptionCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
