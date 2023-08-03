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
    [Route("api/v{version:apiVersion}/color")]
    public class ColorController : ControllerBase
    {
        private readonly IColorApplicationService _colorApplicationService;

        public ColorController(IColorApplicationService colorApplicationService)
        {
            _colorApplicationService = colorApplicationService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ReadProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _colorApplicationService.GetByNameAsync(name);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterNewColorInputModel color)
        {
            var response = await _colorApplicationService.RegisterAsync(color);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }
    }
}