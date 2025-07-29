using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IProductServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class ProductVariantController : Controller
    {
        private readonly IProductVariantService _productVariantService;

        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpGet]
        public async Task<RequestResult<List<ProductVariantDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _productVariantService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<ProductVariantDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _productVariantService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> AddAsync(ProductVariantDto dto, CancellationToken cancellationToken)
        {
            return await _productVariantService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> UpdateAsync(ProductVariantDto dto, CancellationToken cancellationToken)
        {
            return await _productVariantService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _productVariantService.DeleteAsync(id, cancellationToken);
        }
    }
}