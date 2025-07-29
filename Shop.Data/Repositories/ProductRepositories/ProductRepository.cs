using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Data.Repositories.ProductRepositories
{
    public class ProductRepository : Repository<Product>, IProductRepository, IScopedDependency
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }
    }
}