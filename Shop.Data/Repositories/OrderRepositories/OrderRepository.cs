using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IOrderRepositories;
using Shop.Domain.Models.Orders;

namespace Shop.Data.Repositories.OrderRepositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository, IScopedDependency
    {
        public OrderRepository(ShopContext context) : base(context)
        {
        }
    }
}