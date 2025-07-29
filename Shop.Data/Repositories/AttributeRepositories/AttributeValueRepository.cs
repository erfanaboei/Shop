using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IAttributeRepositories;
using Shop.Domain.Models.Attributes;

namespace Shop.Data.Repositories.AttributeRepositories
{
    public class AttributeValueRepository : Repository<AttributeValue>, IAttributeValueRepository, IScopedDependency
    {
        public AttributeValueRepository(ShopContext context) : base(context)
        {
        }
    }
}