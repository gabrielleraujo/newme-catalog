using FluentValidation.Results;
using AutoMapper;
using MediatR;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.Interface;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Services
{
    public class SizeApplicationService : ISizeApplicationService
    {
        private readonly IMapper _mapper;
        private readonly ISizeQueryRepository _sizeQueryRepository;
        private readonly IMediator _mediator;

        public SizeApplicationService(IMapper mapper,
                                      ISizeQueryRepository sizeQueryRepository,
                                      IMediator mediator)
        {
            _mapper = mapper;
            _sizeQueryRepository = sizeQueryRepository;
            _mediator = mediator;
        }

        public async Task<ReadProductViewModel> GetByNameAsync(string name)
        {
            return _mapper.Map<ReadProductViewModel>(await _sizeQueryRepository.GetByNameAsync(name));
        }

        public async Task<ValidationResult> RegisterAsync(RegisterNewSizeInputModel sizeViewModel)
        {
            var sizeCommand = _mapper.Map<RegisterNewSizeCommand>(sizeViewModel);
            return await _mediator.Send(sizeCommand);
        }
    }
}