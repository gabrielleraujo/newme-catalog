using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Commands;
using Microsoft.Extensions.Logging;
using Newme.Catalog.Application.Services.AmazonS3.Externals;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.Services.Externals.AmazonS3.Models;

namespace Newme.Catalog.Domain.CommandHandlers
{
    public class UploadProductImagesCommandHandler : 
        CommandHandler<UploadProductImagesCommandHandler>, 
        IRequestHandler<UploadProductImagesCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IProductQueryRepository _repository;
        private readonly IAmazonS3Service _amazonS3Servise;

        public UploadProductImagesCommandHandler(
            IMediator mediator,
            ILogger<UploadProductImagesCommandHandler> logger,
            IProductQueryRepository repository,
            IAmazonS3Service amazonS3Servise) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
            _amazonS3Servise = amazonS3Servise;
        }

        public async Task<ValidationResult> Handle(UploadProductImagesCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UploadProductImagesCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid");
                return command.ValidationResult;
            }

            var product = await _repository.GetByIdAsync(command.Id);

            if (product == null)
            {
                AddError("Product not found");
                return ValidationResult;
            }

            var images = new List<string>();
            foreach(var item in command.Images)
            {
                var archive = product.CreateImagePath();
                product.AddImage(archive);

                var uploadFile = await _amazonS3Servise.UploadFileAsync(
                    AmazonS3ConfigModel.Buket, 
                    archive, 
                    item);
                
                if(!uploadFile) AddError($"Fail in add image key {archive}");
            }

            await _repository.AddImagesAsync(command.Id, product.Images!.ToList());

            _logger.LogInformation($"{nameof(UploadProductImagesCommandHandler)} successfully completed");

            return ValidationResult;
        }
    }
}
