using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class RegisterPromotionToProductInputModel
    {
        [Required(ErrorMessage = "The id is Required")]
        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The start date is Required")]
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "The end date is Required")]
        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "The percentage is Required")]
        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
        
        [Required(ErrorMessage = "The currency is Required")]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        
        [Required(ErrorMessage = "The amount is Required")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The description is Required")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}