using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Newme.Catalog.Application.InputModels
{
    public class UploadProductImagesInputModel
    {
        [Required]
        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [JsonPropertyName("images")]
        public IList<IFormFile> Images { get; set; }
    }
}