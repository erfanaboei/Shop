using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.ICategoryRepositories;
using Shop.Domain.Models.Categories;

namespace Shop.Data.Repositories.CategoryRepositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository, IScopedDependency
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
        }
    }
}