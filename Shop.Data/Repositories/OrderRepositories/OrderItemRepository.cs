using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IOrderRepositories;
using Shop.Domain.Models.Orders;

namespace Shop.Data.Repositories.OrderRepositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository, IScopedDependency
    {
        public OrderItemRepository(ShopContext context) : base(context)
        {
        }
    }
}