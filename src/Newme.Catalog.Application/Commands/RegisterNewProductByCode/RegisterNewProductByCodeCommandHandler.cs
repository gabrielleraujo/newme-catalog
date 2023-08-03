using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterNewProductByCodeCommandHandler : 
        CommandHandler<RegisterNewProductByCodeCommandHandler>, 
        IRequestHandler<RegisterNewProductByCodeCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterNewProductByCodeCommandHandler(
            IMediator mediator,
            ILogger<RegisterNewProductByCodeCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(RegisterNewProductByCodeCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterNewProductByCodeCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var similarProduct = await _repositoryCommand.FindByAsync<Product>(x => x.Sku.value == command.Sku);

            if (similarProduct == null)
            {
                AddError("Has no one product registered with this sku {command.Code}.");
                return command.ValidationResult;
            }

            var product = new Product(
                new ProductId(Guid.NewGuid()),
                similarProduct.Name, 
                new Sku(command.Sku),
                new Money(
                    similarProduct.Price.Currency, 
                    similarProduct.Price.Amount),
                similarProduct.Description, 
                new DifferentialGroup(
                    similarProduct.DifferentialGroup.category, 
                    new ColorId(command.ColorId), 
                    new SizeId(command.SizeId),
                    similarProduct.DifferentialGroup.gender),
                command.Stock);

            var productWithDifferentialExist = await _repositoryCommand.FindByAsync<Product>(x => 
                x.DifferentialGroup == product.DifferentialGroup
                && x.Sku == product.Sku);
                
            if (productWithDifferentialExist != null)
            {
                AddError("This product is already registered.");
                return ValidationResult;
            }

            await _repositoryCommand.RegisterProductAsync(product);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new RegisteredNewProductEvent(
                    product: product,
                    detail: "registering new product by an existing code product."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(RegisterNewProductCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}