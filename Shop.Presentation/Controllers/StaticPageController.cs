using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IStaticPageServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.StaticPageDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class StaticPageController : Controller
    {
        private readonly IStaticPageService _staticPageService;

        public StaticPageController(IStaticPageService staticPageService)
        {
            _staticPageService = staticPageService;
        }

        [HttpGet]
        public async Task<RequestResult<List<StaticPageDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _staticPageService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<StaticPageDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _staticPageService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> AddAsync(StaticPageDto dto, CancellationToken cancellationToken)
        {
            return await _staticPageService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> UpdateAsync(StaticPageDto dto, CancellationToken cancellationToken)
        {
            return await _staticPageService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _staticPageService.DeleteAsync(id, cancellationToken);
        }
    }
}