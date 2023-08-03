using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class GetCatalogItemViewModel
    {
        public GetCatalogItemViewModel(Guid id, Guid sku, string name, string currency, decimal amount, string description, ReadGenderViewModel gender, ReadCategoryViewModel category, ReadColorViewModel color, ReadSizeViewModel size, int stock, ReadPromotionViewModel? promotion, List<string>? images)
        {
            Id = id;
            Sku = sku;
            Name = name;
            Currency = currency;
            Amount = amount;
            Description = description;
            Gender = gender;
            Category = category;
            Color = color;
            Size = size;
            Stock = stock;
            Promotion = promotion;
            Images = images;
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("sku")]
        public Guid Sku { get; set; }

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