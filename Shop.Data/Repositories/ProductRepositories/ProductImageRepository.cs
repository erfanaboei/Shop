using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Data.Repositories.ProductRepositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository, IScopedDependency
    {
        public ProductImageRepository(ShopContext context) : base(context)
        {
        }
    }
}