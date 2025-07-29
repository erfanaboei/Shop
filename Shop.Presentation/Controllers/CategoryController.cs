using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.ICategoryServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.CategoryDataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [Route("Api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<RequestResult<List<CategoryDto>>> GetList(CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _categoryService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> Add(CategoryDto dto, CancellationToken cancellationToken)
        {
            return await _categoryService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> Update(CategoryDto dto, CancellationToken cancellationToken)
        {
            return await _categoryService.UpdateAsync(dto, cancellationToken);
        }
        
        [HttpGet("[action]")]
        public async Task<RequestResult<List<OptionDto>>> GetOptions(CancellationToken cancellationToken)
        {
            return await _categoryService.GetSelectListAsync(cancellationToken);
        }
    }
}