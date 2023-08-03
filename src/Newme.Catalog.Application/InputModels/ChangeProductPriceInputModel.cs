using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.InputModels
{
    public class ChangeProductPriceInputModel
    {
        [Required(ErrorMessage = "The id is Required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The currency is Required")]
        [MinLength(1)]
        [MaxLength(3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "The price is Required")]
        [Range(1.0, double.MaxValue)]
        [JsonPropertyName("price")]
        public decimal Amount { get; set; }
    }
}
