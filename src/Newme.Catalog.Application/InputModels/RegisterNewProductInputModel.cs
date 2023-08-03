using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class RegisterNewProductInputModel
    {
        [Required(ErrorMessage = "The name is Required")]
        [MinLength(2)]
        [MaxLength(150)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The currency is Required")]
        [MinLength(1)]
        [MaxLength(3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "The amount is Required")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The description is Required")]
        [MinLength(2)]
        [MaxLength(500)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The gender is Required")]
        [JsonPropertyName("gender")]
        public Guid Gender { get; set; }

        [Required(ErrorMessage = "The category is Required")]
        [JsonPropertyName("category")]
        public Guid Category { get; set; }

        [Required(ErrorMessage = "The color is Required")]
        [JsonPropertyName("color")]
        public Guid Color { get; set; }

        [Required(ErrorMessage = "The size is Required")]
        [JsonPropertyName("size")]
        public Guid Size { get; set; }

        [Required(ErrorMessage = "The stock quantity is Required")]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}