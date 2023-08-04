using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newme.Catalog.Application.InputModels;
using Newme.Catalog.Application.Interface;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;

        public ProductController(
            IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterNewProductInputModel product)
        {
            var response = await _productApplicationService.RegisterAsync(product);
            return response.Errors.Count == 0 ? Ok() : BadRequest(response);
        }

        [HttpPost("product/code")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterByCodeInputModel([FromBody] RegisterNewProductDifferantialByCodeInputModel product)
        {
            var response = await _productApplicationService.RegisterByCodeInputModel(product);
            return response.Errors.Count == 0 ? Ok() : BadRequest(response);
        }

        [HttpGet("catalog")]
        [ProducesResponseType(typeof(GetCatalogViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCatalog()
        {
            var response = await _productApplicationService.GetCatalog();
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet("catalog-by-filter")]
        [ProducesResponseType(typeof(GetCatalogViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCatalogByFilter([FromQuery] GetCatalogByFilterInputModel inputModel)
        {
            var response = await _productApplicationService.GetCatalogByFilter(inputModel);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetCatalogViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _productApplicationService.GetByNameAsync(name);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPatch("deactivate/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeactivateProduct([FromRoute] Guid id)
        {
            var response = await _productApplicationService.DeactivateAsync(id);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }

        [HttpDelete("remove/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveProduct([FromRoute] Guid id)
        {
            var response = await _productApplicationService.RemoveAsync(id);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }

        [HttpPatch("fix-name-description")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] FixProductNameAndDescriptionInputModel product)
        {
            var response = await _productApplicationService.FixProductNameAndDescriptionInputModel(product);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }

        [HttpPatch("change-price")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ChangeProductPriceInputModel product)
        {
            var response = await _productApplicationService.ChangeProductPriceInputModel(product);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }

        [HttpPost("promotion")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] RegisterPromotionToProductInputModel promotion)
        {
            var response = await _productApplicationService.RegisterPromotionToProductInputModel(promotion);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }

        [HttpPost("upload-images")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UploadImages([FromForm] UploadProductImagesInputModel inputModel)
        {
            var response = await _productApplicationService.UploadImages(inputModel);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }
    }
}