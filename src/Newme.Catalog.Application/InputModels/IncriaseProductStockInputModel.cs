using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class IncreaseProductStockInputModel
    {
        [Required(ErrorMessage = "The name is Required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The stock quantity is Required")]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("quantity")]
        public int Stock { get; set; }
    }
}