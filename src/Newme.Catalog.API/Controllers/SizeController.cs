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
    [Route("api/v{version:apiVersion}/size")]
    public class SizeController : ControllerBase
    {
        private readonly ISizeApplicationService _sizeApplicationService;

        public SizeController(ISizeApplicationService sizeApplicationService)
        {
            _sizeApplicationService = sizeApplicationService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ReadProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _sizeApplicationService.GetByNameAsync(name);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterNewSizeInputModel size)
        {
            var response = await _sizeApplicationService.RegisterAsync(size);
            return response.Errors.Count == 0 ? NoContent() : BadRequest(response);
        }
    }
}