using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IOrderServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.OrderDataTransferObjects;

namespace Shop.Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<RequestResult<List<OrderDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _orderService.GetAllDtoAsync(cancellationToken);
        }

        [HttpGet("id")]
        public async Task<RequestResult<OrderDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _orderService.GetDtoByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<RequestResult> AddAsync(OrderDto dto, CancellationToken cancellationToken)
        {
            return await _orderService.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        public async Task<RequestResult> UpdateAsync(OrderDto dto, CancellationToken cancellationToken)
        {
            return await _orderService.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public async Task<RequestResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _orderService.DeleteAsync(id, cancellationToken);
        }
    }
}