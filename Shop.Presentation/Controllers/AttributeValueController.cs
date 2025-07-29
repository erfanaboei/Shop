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
    public class AttributeValueController : Controller
    {
        private readonly IAttributeValueService _attributeValueService;

        public AttributeValueController(IAttributeValueService attributeValueService)
        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public async Task<RequestResult<List<AttributeValueDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _attributeValueService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<AttributeValueDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _attributeValueService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> AddAsync(AttributeValueDto dto, CancellationToken cancellationToken)
        {
            return await _attributeValueService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> UpdateAsync(AttributeValueDto dto, CancellationToken cancellationToken)
        {
            return await _attributeValueService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _attributeValueService.DeleteAsync(id, cancellationToken);
        }
    }
}