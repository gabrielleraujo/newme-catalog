using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Events;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterNewProductCommandHandler : 
        CommandHandler<RegisterNewProductCommandHandler>, 
        IRequestHandler<RegisterNewProductCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterNewProductCommandHandler(
            IMediator mediator,
            ILogger<RegisterNewProductCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(RegisterNewProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterNewProductCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var product = new Product(
                new ProductId(Guid.NewGuid()),
                command.Name, 
                new Sku(Guid.NewGuid()),
                new Money(command.Currency, command.Amount),
                command.Description, 
                new DifferentialGroup(
                    new CategoryId(command.CategoryId), 
                    new ColorId(command.ColorId), 
                    new SizeId(command.SizeId),
                    new GenderId(command.GenderId)),
                command.Stock);

            var productWithDifferentialExist = await _repositoryCommand.GetByAsync<Product>(x => 
                x.DifferentialGroup == product.DifferentialGroup
                && x.Name == product.Name);
                
            if (productWithDifferentialExist.Count > 0)
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
                    detail: "registering new product."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(RegisterNewProductCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}