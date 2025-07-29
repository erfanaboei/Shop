using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.IUserRepositories;
using Shop.Domain.Models.Users;

namespace Shop.Data.Repositories.UserRepositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        public UserRepository(ShopContext context) : base(context)
        {
        }
    }
}