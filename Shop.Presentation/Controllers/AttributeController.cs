using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IAttributeServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class AttributeController : Controller
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public async Task<RequestResult<List<AttributeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _attributeService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<AttributeDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _attributeService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> AddAsync(AttributeDto dto, CancellationToken cancellationToken)
        {
            return await _attributeService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> UpdateAsync(AttributeDto dto, CancellationToken cancellationToken)
        {
            return await _attributeService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _attributeService.DeleteAsync(id, cancellationToken);
        }
    }
}