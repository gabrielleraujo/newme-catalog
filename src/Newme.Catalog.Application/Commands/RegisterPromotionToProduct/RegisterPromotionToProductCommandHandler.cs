using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Events;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterPromotionToProductCommandHandler : 
        CommandHandler<RegisterPromotionToProductCommandHandler>, 
        IRequestHandler<RegisterPromotionToProductCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterPromotionToProductCommandHandler(
            IMediator mediator,
            ILogger<RegisterPromotionToProductCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }


        public async Task<ValidationResult> Handle(RegisterPromotionToProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterPromotionToProductCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var product = await _repositoryCommand.FindByAsync<Product>(x => x.Id.value == command.ProductId);

             if (product == null)
            {
                AddError("Product not found");
                return ValidationResult;
            }

            if (product.Promotion != null)
            {
                AddError("This product already has a promotion, please remove the current promotion and register a new one.");
                return ValidationResult;
            }

            var promotion = new Promotion(
                new PromotionId(Guid.NewGuid()), 
                new ProductId(command.ProductId), 
                command.Name,
                new Period(command.Start, command.End),
                new Discount(command.Percentage, 
                    new Money(
                        "R$", 
                        command.Amount)),
                command.Description);

            product.AddPromotion(promotion);

            _repositoryCommand.Update(product);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new ChangedProductEvent(
                    product: product,
                    detail: "registered promotion to product."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(FixProductNameAndDescriptionCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}