using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class RegisterNewProductDifferantialByCodeInputModel
    {
        [Required(ErrorMessage = "The sku code is Required")]
        [JsonPropertyName("sku")]
        public Guid Sku { get; set; }

        [Required(ErrorMessage = "The color is Required")]
        [JsonPropertyName("color")]
        public Guid ColorId { get; set; }

        [Required(ErrorMessage = "The size is Required")]
        [JsonPropertyName("size")]
        public Guid SizeId { get; set; }

        [Required(ErrorMessage = "The gender is Required")]
        [JsonPropertyName("gender")]
        public Guid GenderId { get; set; }

        [Required(ErrorMessage = "The stock quantity is Required")]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}