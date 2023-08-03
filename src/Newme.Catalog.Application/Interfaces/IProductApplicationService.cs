using FluentValidation.Results;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Interface
{
    public interface IProductApplicationService
    {
        Task<IEnumerable<ReadProductViewModel>> GetAllAsync();
        Task<GetCatalogViewModel> GetCatalog();
        Task<GetCatalogViewModel> GetByNameAsync(string name);
        Task<ValidationResult> RegisterAsync(RegisterNewProductInputModel inputModel);
        Task<ValidationResult> DeactivateAsync(Guid id);
        Task<ValidationResult> RemoveAsync(Guid id);
        Task<ValidationResult> ChangeProductPriceInputModel(ChangeProductPriceInputModel viewModel);
        Task<ValidationResult> FixProductNameAndDescriptionInputModel(FixProductNameAndDescriptionInputModel inputModel);
        Task<ValidationResult> RegisterPromotionToProductInputModel(RegisterPromotionToProductInputModel inputModel);
        Task<ValidationResult> RegisterByCodeInputModel(RegisterNewProductDifferantialByCodeInputModel inputModel);
        Task<ValidationResult> UploadImages(UploadProductImagesInputModel inputModel);
    }
}