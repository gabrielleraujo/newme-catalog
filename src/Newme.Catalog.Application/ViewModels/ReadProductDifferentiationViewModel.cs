using System.Text.Json.Serialization;

namespace Newme.Catalog.Application.ViewModels
{
    public class ReadProductDifferentiationViewModel
    {
        public ReadProductDifferentiationViewModel(
            Guid id, 
            string name)
        {
            Id = id;
            Name = name;
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}