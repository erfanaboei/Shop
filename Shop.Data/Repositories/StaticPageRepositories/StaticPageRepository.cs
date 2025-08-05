using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IStaticPageRepositories;
using Shop.Domain.Models.StaticPages;

namespace Shop.Data.Repositories.StaticPageRepositories
{
    public class StaticPageRepository: Repository<StaticPage>, IStaticPageRepository, IScopedDependency
    {
        public StaticPageRepository(ShopContext context) : base(context)
        {
        }
    }
}