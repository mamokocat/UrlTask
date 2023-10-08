using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyUrl.API.Mappers;
using TinyUrl.API.Models;
using TinyUrl.API.Recources;
using TinyUrl.Common.Interfaces;
using TinyUrl.Common.Models;
using TinyUrl.Services.Interfaces;
using TinyUrl.Services.Models;

namespace TinyUrl.API.Controllers
{
    [Route("urls")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService urlService;
        private readonly IUrlViewMapper mapper;
        public UrlController(IUrlService urlService, IUrlViewMapper mapper)
        {
            this.urlService = urlService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUrlsAsync(int skip, int take) {

            ILookupModel<IUrlModel> result = await urlService.FilterAsync(skip, take);

            var mappedModel = mapper.MapUrlLookup(result);

            return this.Ok(mappedModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUrlAsync([FromRoute] int id)
        {

            Result<IUrlModel> result = await urlService.GetUrlAsync(id);


            return result.IsSuccess ? this.Ok(result.Value) : this.BadRequest(ResponseMessages.UrlNotExist);
        }

        [HttpGet("validatePath")]
        public async Task<IActionResult> ValidatePathAsync(string path)
        {

            Result<List<string>> result = await urlService.ValidatePathAsync(path);


            return this.Ok(mapper.MapValidationResult(result));
        }

        [HttpGet("redirect/{tinyUrl}")]
        public async Task<IActionResult> GetRedirectUrlAsync([FromRoute]string tinyUrl)
        {
            Result<string> result = await this.urlService.GetRedirectUrlAsync(tinyUrl);
            return result.IsSuccess ? this.Ok(result.Value) : this.BadRequest(ResponseMessages.UrlNotExist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            Result<bool> result = await this.urlService.DeleteAsync(id);

            return result.IsSuccess ? this.Ok() : this.BadRequest(ResponseMessages.UrlNotExist);
        }

        [HttpPut]
        public async Task<IActionResult> AddAsync([FromBody] AddEditViewModel model)
        {
            Result<List<string>> result = await this.urlService.AddAsync(model);

            return result.IsSuccess ? this.Ok() : this.BadRequest(mapper.MapRecources(result.Value));
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromBody] AddEditViewModel model)
        {
            Result<List<string>> result = await this.urlService.EditAsync(model, id);

            return result.IsSuccess ? this.Ok() : this.BadRequest(mapper.MapRecources(result.Value));
        }
    }
}
