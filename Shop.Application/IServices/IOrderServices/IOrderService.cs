using Shop.Domain.DataTransferObjects.OrderDataTransferObjects;
using Shop.Domain.Models.Orders;

namespace Shop.Application.IServices.IOrderServices
{
    public interface IOrderService : IService<Order, OrderDto>
    {
        
    }
}