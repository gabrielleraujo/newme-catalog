using Newme.Catalog.Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Events;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class IncreaseProductStockCommandHandler : 
        CommandHandler<IncreaseProductStockCommandHandler>, 
        IRequestHandler<IncreaseProductStockCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public IncreaseProductStockCommandHandler(
            IMediator mediator,
            ILogger<IncreaseProductStockCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(IncreaseProductStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(IncreaseProductStockCommandHandler)} starting");

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

            product.IncreaseStock(command.Stock);

            _repositoryCommand.Update(product);   
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new ChangedProductEvent(
                    product: product,
                    detail: $"updated product stock to {command.Stock}."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(IncreaseProductStockCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
