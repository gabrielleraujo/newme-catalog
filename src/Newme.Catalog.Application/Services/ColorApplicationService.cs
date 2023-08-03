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
    public class ColorApplicationService : IColorApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IColorQueryRepository _colorQueryRepository;
        private readonly IMediator _mediator;

        public ColorApplicationService(IMapper mapper,
                                       IColorQueryRepository colorQueryRepository,
                                       IMediator mediator)
        {
            _mapper = mapper;
            _colorQueryRepository = colorQueryRepository;
            _mediator = mediator;
        }

        public async Task<ReadProductViewModel> GetByNameAsync(string name)
        {
            return _mapper.Map<ReadProductViewModel>(await _colorQueryRepository.GetByNameAsync(name));
        }

        public async Task<ValidationResult> RegisterAsync(RegisterNewColorInputModel colorViewModel)
        {
            var colorCommand = _mapper.Map<RegisterNewColorCommand>(colorViewModel);
            return await _mediator.Send(colorCommand);
        }
    }
}