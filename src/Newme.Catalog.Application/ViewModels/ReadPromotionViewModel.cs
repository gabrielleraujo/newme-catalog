using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class ReadPromotionViewModel
    {
        public ReadPromotionViewModel(Guid productId, string name, DateTime start, DateTime end, decimal percentage, string currency, decimal amount, string description)
        {
            ProductId = productId;
            Name = name;
            Start = start;
            End = end;
            Percentage = percentage;
            Currency = currency;
            Amount = amount;
            Description = description;
        }

        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}