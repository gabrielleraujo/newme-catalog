using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.InputModels
{
    public class GetCatalogByFilterInputModel
    {
        [JsonPropertyName("categories")]
        public IList<string>? Categories { get; set; } = new List<string>();

        [JsonPropertyName("colors")]
        public IList<string>? Colors { get; set; } = new List<string>();

        [JsonPropertyName("sizes")]
        public IList<string>? Sizes { get; set; } = new List<string>();

        [JsonPropertyName("genders")]
        public IList<string>? Genders { get; set; } = new List<string>();

        [JsonPropertyName("min_price")]
        public decimal MinPrice { get; set; } = 0.0m;

        [JsonPropertyName("max_price")]
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}