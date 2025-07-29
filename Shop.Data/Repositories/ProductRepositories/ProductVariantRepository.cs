using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Data.Repositories.ProductRepositories
{
    public class ProductVariantRepository : Repository<ProductVariant>, IProductVariantRepository, IScopedDependency
    {
        public ProductVariantRepository(ShopContext context) : base(context)
        {
        }
    }
}