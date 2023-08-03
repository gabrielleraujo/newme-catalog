using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newme.Catalog.Application.InputModels
{
    public class RegisterNewProductDifferentialInputModel
    {
        public RegisterNewProductDifferentialInputModel(string name)
        {
            Name = name;
        }

        [Required(ErrorMessage = "The name is Required")]
        [MinLength(1)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}