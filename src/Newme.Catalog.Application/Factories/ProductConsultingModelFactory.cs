using Microsoft.Extensions.Logging;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Application.Factories
{
    public class ProductConsultingModelFactory : IProductConsultingModelFactory
    {
        private readonly ILogger<ProductConsultingModelFactory> _logger;
        private readonly IGenderQueryRepository _genderRepositoryQuery;
        private readonly ICategoryQueryRepository _categoryRepositoryQuery;
        private readonly IColorQueryRepository _colorRepositoryQuery;
        private readonly ISizeQueryRepository _sizeRepositoryQuery;

        public ProductConsultingModelFactory(
            ILogger<ProductConsultingModelFactory> logger,
            IProductQueryRepository repositoryQuery,
            IGenderQueryRepository genderRepositoryQuery,
            ICategoryQueryRepository categoryRepositoryQuery,
            IColorQueryRepository colorRepository,
            ISizeQueryRepository sizeRepository)
        {
            _logger = logger;
            _genderRepositoryQuery = genderRepositoryQuery;
            _categoryRepositoryQuery = categoryRepositoryQuery;
            _colorRepositoryQuery = colorRepository;
            _sizeRepositoryQuery = sizeRepository;
        }

        public async Task<ProductConsultingModel> Build(Product product)
        {
            var genders = await _genderRepositoryQuery.GetAllAsync();
            var categories = await _categoryRepositoryQuery.GetAllAsync();
            var colors = await _colorRepositoryQuery.GetAllAsync();
            var sizes = await _sizeRepositoryQuery.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                _logger.LogError("Categories is null, no one category has founf.");
                throw new ArgumentException();
            }
            if (colors == null || !colors.Any())
            {
                _logger.LogError("Colors is null, no one colors has founf.");
                throw new ArgumentException();
            }
            if (sizes == null || !sizes.Any())
            {
                _logger.LogError("Sizes is null, no one sizes has founf.");
                throw new ArgumentException();
            }

            return ProductConsultingModel.MapFromDomain(
                product,
                BuildGender(genders, product),
                BuildCategory(categories, product),
                BuildColor(colors, product),
                BuildSize(sizes, product)
            );
        }

        private static GenderConsultingModel BuildGender(IEnumerable<GenderConsultingModel> genders, Product product)
        {
            var genderId = product.DifferentialGroup.gender.value;
            return new GenderConsultingModel(
                id: genderId,
                name: genders.FirstOrDefault(x => x.Id == genderId)!.Name);
        }
        
        private static SizeConsultingModel BuildSize(IEnumerable<SizeConsultingModel> sizes, Product product)
        {
            var sizeId = product.DifferentialGroup.size.value;
            return new SizeConsultingModel(
                id: sizeId,
                name: sizes.FirstOrDefault(x => x.Id == sizeId)!.Name);
        }

        private static ColorConsultingModel BuildColor(IEnumerable<ColorConsultingModel> colors, Product product)
        {
            var colorId = product.DifferentialGroup.color.value;
            return new ColorConsultingModel(
                id: colorId,
                name: colors.FirstOrDefault(x => x.Id == colorId)!.Name);
        }

        private static CategoryConsultingModel BuildCategory(IEnumerable<CategoryConsultingModel> categories, Product product)
        {
            var categoryId = product.DifferentialGroup.category.value;
            return new CategoryConsultingModel(
                id: categoryId,
                name: categories.FirstOrDefault(x => x.Id == categoryId)!.Name);
        }
    }
}