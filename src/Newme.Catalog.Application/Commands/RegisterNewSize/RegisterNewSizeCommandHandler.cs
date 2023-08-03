using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Events;
using Microsoft.Extensions.Logging;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class RegisterNewSizeCommandHandler : 
        CommandHandler<RegisterNewSizeCommandHandler>, 
        IRequestHandler<RegisterNewSizeCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductCommandRepository _repositoryCommand;

        public RegisterNewSizeCommandHandler(
            IMediator mediator,
            ILogger<RegisterNewSizeCommandHandler> logger,
            IProductCommandRepository repositoryCommand) : base(logger)
        {
            _mediator = mediator;
            _repositoryCommand = repositoryCommand;
        }

        public async Task<ValidationResult> Handle(RegisterNewSizeCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(RegisterNewSizeCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var categoryByName = await _repositoryCommand.FindByAsync<Size>(x => x.Name == command.Name);
            if (categoryByName != null)
            {
                AddError("This size is already registered");
                return ValidationResult;
            }

            var size = new Size(new SizeId(Guid.NewGuid()), command.Name);

            await _repositoryCommand.RegisterSizeAsync(size);
            var isCommitSuccess = await _repositoryCommand.Commit();          

            if (isCommitSuccess) 
            {
                var changeProductPriceEvent = new RegisteredNewSizeEvent(
                    size: size,
                    detail: "registered new size."
                );
                await _mediator.Publish(changeProductPriceEvent).ConfigureAwait(false);
            }

            _logger.LogInformation($"{nameof(RegisterNewCategoryCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}