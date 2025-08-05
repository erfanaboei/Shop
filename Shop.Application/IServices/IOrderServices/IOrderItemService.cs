using Shop.Domain.DataTransferObjects.OrderDataTransferObjects;
using Shop.Domain.Models.Orders;

namespace Shop.Application.IServices.IOrderServices
{
    public interface IOrderItemService : IService<OrderItem, OrderItemDto>
    {
        
    }
}