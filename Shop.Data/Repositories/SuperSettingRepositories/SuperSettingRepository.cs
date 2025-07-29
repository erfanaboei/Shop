using Shop.Data.Context;
using Shop.Domain;
using Shop.Domain.IRepositories.ISuperSettingRepositories;
using Shop.Domain.Models.SuperSettings;

namespace Shop.Data.Repositories.SuperSettingRepositories
{
    public class SuperSettingRepository : Repository<SuperSetting>, ISuperSettingRepository, IScopedDependency
    {
        public SuperSettingRepository(ShopContext context) : base(context)
        {
        }
    }
}