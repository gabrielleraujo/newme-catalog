using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class FixProductNameAndDescriptionInputModel
    {
        [Required(ErrorMessage = "The id is Required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}