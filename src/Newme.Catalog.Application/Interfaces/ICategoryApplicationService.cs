using FluentValidation.Results;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Interface
{
    public interface ICategoryApplicationService
    {
        Task<ReadProductViewModel> GetByNameAsync(string name);
        Task<ValidationResult> RegisterAsync(RegisterNewCategoryInputModel viewModel);
    }
}