using Shop.Application.IServices.IOrderServices;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.OrderDataTransferObjects;
using Shop.Domain.IRepositories;
using Shop.Domain.Models.Orders;

namespace Shop.Application.Services.OrderServices
{
    public class OrderService: Service<Order, OrderDto>, IOrderService, IScopedDependency
    {
        public OrderService(IRepository<Order> repository) : base(repository)
        {
        }
    }
}