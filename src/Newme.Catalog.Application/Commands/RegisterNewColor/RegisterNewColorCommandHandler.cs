using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterNewColorCommandHandler : 
        CommandHandler<RegisterNewColorCommandHandler>, 
        IRequestHandler<RegisterNewColorCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterNewColorCommandHandler(
            IMediator mediator,
            ILogger<RegisterNewColorCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }


        public async Task<ValidationResult> Handle(RegisterNewColorCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterNewCategoryCommandHandler)} starting");
            
            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var categoryByName = await _repositoryCommand.FindByAsync<Color>(x => x.Name == command.Name);
            if (categoryByName != null)
            {
                AddError("This color is already registered");
                return ValidationResult;
            }

            var color = new Color(new ColorId(Guid.NewGuid()), command.Name);

            await _repositoryCommand.RegisterColorAsync(color);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new RegisteredNewColorEvent(
                    color: color,
                    detail: "registered new color."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(RegisterNewCategoryCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}