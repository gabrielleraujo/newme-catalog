using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class GetCatalogBySkuViewModel
    {
        public GetCatalogBySkuViewModel(
            Guid sku, 
            List<GetCatalogItemViewModel> products)
        {
            Sku = sku;
            Products = products;
        }

        [JsonPropertyName("sku")]
        public Guid Sku { get; private set; }

        [JsonPropertyName("products")]
        public List<GetCatalogItemViewModel> Products { get; private set; }
    }
}
