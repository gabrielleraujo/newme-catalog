using Newme.Catalog.Application.ViewModels;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.Queries
{
    public static class GetCatalogViewModelFactory
    {
        public static GetCatalogViewModel Build(
            IEnumerable<ProductConsultingModel>? products)
        {
            if (products == null || !products.Any())
            {
                return new(new());
            }

            var catalog = products.GroupBy(x => x.Sku);

            List<GetCatalogItemViewModel> productItems = new();
            List<GetCatalogBySkuViewModel> productsBySku = new();

            foreach (var group in catalog)
            {
                foreach (var product in group)
                {
                    if (product == null) continue;
                    productItems.Add(
                        new GetCatalogItemViewModel(
                        id: product.Id,
                        sku: product.Sku,
                        name: product.Name,
                        currency: product.Currency,
                        amount: product.Amount,
                        description: product.Description,
                        gender: new(product.Gender.Id, product.Gender.Name),
                        category: new(product.Category.Id, product.Category.Name),
                        color: new(product.Color.Id, product.Color.Name),
                        size: new(product.Size.Id, product.Size.Name),
                        stock: product.Stock,
                        promotion: BuildPromotion(product),
                        images: BuildImages(product)
                    ));
                }
                productsBySku.Add(new GetCatalogBySkuViewModel(
                    group.FirstOrDefault()!.Sku, productItems));
                productItems = new();
            }
            return new GetCatalogViewModel(productsBySku);
        }

        private static ReadPromotionViewModel? BuildPromotion(ProductConsultingModel product)
        {
            return product.Promotion == null ? null : new ReadPromotionViewModel(
                productId: product.Promotion.Id,
                name: product.Promotion.Name,
                start: product.Promotion.Start,
                end: product.Promotion.End,
                percentage: product.Promotion.Percentage,
                currency: product.Promotion.Currency,
                amount: product.Promotion.Amount,
                description: product.Promotion.Description);
        }

        private static List<string>? BuildImages(ProductConsultingModel product)
        {
            List<string> result = new();

            if (product.Images == null) return result;

            foreach(var image in product.Images)
            {
                result.Add(image);
            }
            return result;
        }
    }
}