using FluentValidation.Results;
using AutoMapper;
using MediatR;
using Newme.Catalog.Application.ViewModels;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Interface;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.InputModels;

namespace Newme.Catalog.Application.Services
{
    public class CategoryApplicationService : ICategoryApplicationService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly IMediator _mediator;

        public CategoryApplicationService(IMapper mapper,
                                         ICategoryQueryRepository categoryQueryRepository,
                                         IMediator mediator)
        {
            _mapper = mapper;
            _categoryQueryRepository = categoryQueryRepository;
            _mediator = mediator;
        }

        public async Task<ReadProductViewModel> GetByNameAsync(string name)
        {
            return _mapper.Map<ReadProductViewModel>(await _categoryQueryRepository.GetByNameAsync(name));
        }

        public async Task<ValidationResult> RegisterAsync(RegisterNewCategoryInputModel categoryViewModel)
        {
            var categoryCommand = _mapper.Map<RegisterNewCategoryCommand>(categoryViewModel);
            return await _mediator.Send(categoryCommand);
        }
    }
}