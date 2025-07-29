using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IRoleRepositories;
using Shop.Domain.Models.Roles;

namespace Shop.Data.Repositories.RoleRepositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository, IScopedDependency
    {
        public RoleRepository(ShopContext context) : base(context)
        {
        }
    }
}