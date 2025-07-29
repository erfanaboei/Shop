using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IAttributeRepositories;
using Shop.Domain.Models.Attributes;

namespace Shop.Data.Repositories.AttributeRepositories
{
    public class AttributeRepository : Repository<Attribute>, IAttributeRepository, IScopedDependency
    {
        public AttributeRepository(ShopContext context) : base(context)
        {
        }
    }
}