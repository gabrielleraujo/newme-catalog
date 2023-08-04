using FluentValidation.Results;
using AutoMapper;
using MediatR;
using Newme.Catalog.Application.ViewModels;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Interface;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.Queries;

namespace Newme.Catalog.Application.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMediator _mediator;

        public ProductApplicationService(
            IMapper mapper,
            IProductQueryRepository productQueryRepository,
            IMediator mediator)
        {
            _mapper = mapper;
            _productQueryRepository = productQueryRepository;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ReadProductViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ReadProductViewModel>>(await _productQueryRepository.GetAllAsync());
        }

        public async Task<GetCatalogViewModel> GetByNameAsync(string name)
        {
            var productCommand = new SearchByNameQuery(name);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> RegisterAsync(RegisterNewProductInputModel productViewModel)
        {
            var productCommand = _mapper.Map<RegisterNewProductCommand>(productViewModel);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> DeactivateAsync(Guid id)
        {
            var productCommand = new DeactivateProductCommand(id);
            return await _mediator.Send(productCommand);
        }
        
        public async Task<ValidationResult> RemoveAsync(Guid id)
        {
            var productCommand = new RemoveProductCommand(id);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> ChangeProductPriceInputModel(ChangeProductPriceInputModel inputModel)
        {
            var productCommand = _mapper.Map<ChangeProductPriceCommand>(inputModel);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> FixProductNameAndDescriptionInputModel(FixProductNameAndDescriptionInputModel inputModel)
        {
            var productCommand = _mapper.Map<FixProductNameAndDescriptionCommand>(inputModel);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> RegisterPromotionToProductInputModel(RegisterPromotionToProductInputModel inputModel)
        {
            var promotionCommand = _mapper.Map<RegisterPromotionToProductCommand>(inputModel);
            return await _mediator.Send(promotionCommand);
        }

        public async Task<GetCatalogViewModel> GetCatalog()
        {
            return await _mediator.Send(new GetCatalogQuery());
        }

        public async Task<GetCatalogViewModel> GetCatalogByFilter(GetCatalogByFilterInputModel inputModel)
        {
            return await _mediator.Send(new GetCatalogByFilterQuery(
                inputModel.Categories,
                inputModel.Colors,
                inputModel.Sizes,
                inputModel.Genders,
                inputModel.MinPrice,
                inputModel.MaxPrice));
        }

        public async Task<ValidationResult> RegisterByCodeInputModel(RegisterNewProductDifferantialByCodeInputModel inputModel)
        {
            var productCommand = _mapper.Map<RegisterNewProductByCodeCommand>(inputModel);
            return await _mediator.Send(productCommand);
        }

        public async Task<ValidationResult> UploadImages(UploadProductImagesInputModel inputModel)
        {
            var productCommand = _mapper.Map<UploadProductImagesCommand>(inputModel);
            return await _mediator.Send(productCommand);
        }
    }
}
