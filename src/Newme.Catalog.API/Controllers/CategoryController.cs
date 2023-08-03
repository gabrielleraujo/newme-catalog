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
    [Route("api/v{version:apiVersion}/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplicationService _categoryApplicationService;

        public CategoryController(ICategoryApplicationService categoryApplicationService)
        {
            _categoryApplicationService = categoryApplicationService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ReadProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _categoryApplicationService.GetByNameAsync(name);
            return Ok(response);
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterNewCategoryInputModel category)
        {
            var response = await _categoryApplicationService.RegisterAsync(category);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }
    }
}