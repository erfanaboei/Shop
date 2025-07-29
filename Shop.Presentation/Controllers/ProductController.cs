using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IProductServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<RequestResult<List<ProductDto>>> GetList(CancellationToken cancellationToken)
        {
            return await _productService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<ProductDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _productService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> Add(ProductDto dto, CancellationToken cancellationToken)
        {
            return await _productService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> Update(ProductDto dto, CancellationToken cancellationToken)
        {
            return await _productService.UpdateAsync(dto, cancellationToken);
        }
        
        [HttpGet("[action]")]
        public async Task<RequestResult<List<OptionDto>>> GetOptions(CancellationToken cancellationToken)
        {
            return await _productService.GetSelectListAsync(cancellationToken);
        }    
    }
}