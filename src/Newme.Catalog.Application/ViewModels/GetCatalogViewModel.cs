using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class GetCatalogViewModel
    {
        public GetCatalogViewModel(List<GetCatalogBySkuViewModel> productsByCode)
        {
            ProductsByCode = productsByCode;
        }

        [JsonPropertyName("products_by_sku")]
        public List<GetCatalogBySkuViewModel> ProductsByCode { get; private set; }
    }
}
