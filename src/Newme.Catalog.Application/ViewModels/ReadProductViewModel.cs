using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class ReadProductViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("gender")]
        public ReadGenderViewModel Gender { get; set; }
        
        [JsonPropertyName("category")]
        public ReadCategoryViewModel Category { get; set; }

        [JsonPropertyName("color")]
        public ReadColorViewModel Color { get; set; }

        [JsonPropertyName("size")]
        public ReadSizeViewModel Size { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("promotion")]
        public ReadPromotionViewModel? Promotion { get; set; }

        [JsonPropertyName("images")]
        public List<string>? Images { get; set; }
    }
}