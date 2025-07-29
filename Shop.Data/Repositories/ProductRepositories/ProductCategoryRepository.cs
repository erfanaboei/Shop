using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Data.Repositories.ProductRepositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository, IScopedDependency
    {
        public ProductCategoryRepository(ShopContext context) : base(context)
        {
        }
    }
}