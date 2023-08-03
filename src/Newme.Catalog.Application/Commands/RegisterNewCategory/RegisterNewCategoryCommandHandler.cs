using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterNewCategoryCommandHandler : 
        CommandHandler<RegisterNewCategoryCommandHandler>, 
        IRequestHandler<RegisterNewCategoryCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterNewCategoryCommandHandler(
            IMediator mediator,
            ILogger<RegisterNewCategoryCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(RegisterNewCategoryCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterNewCategoryCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var categoryByName = await _repositoryCommand.FindByAsync<Category>(x => x.Name == command.Name);
            if (categoryByName != null)
            {
                AddError("This category is already registered");
                return ValidationResult;
            }

            var category = new Category(new CategoryId(Guid.NewGuid()), command.Name);

            await _repositoryCommand.RegisterCategoryAsync(category);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new RegisteredNewCategoryEvent(
                    category: category,
                    detail: "registered new category."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(RegisterNewCategoryCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
