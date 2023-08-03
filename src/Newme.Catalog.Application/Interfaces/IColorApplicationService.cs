using FluentValidation.Results;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Interface
{
    public interface IColorApplicationService
    {
        Task<ReadProductViewModel> GetByNameAsync(string name);
        Task<ValidationResult> RegisterAsync(RegisterNewColorInputModel viewModel);
    }
}