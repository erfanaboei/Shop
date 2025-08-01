using Shop.Application.IServices.ISuperSettingServices;
using Shop.Application.Mappings.SuperSettingMappings;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.SuperSettingDataTransferObjects;
using Shop.Domain.IRepositories.ISuperSettingRepositories;
using Shop.Domain.Models.SuperSettings;

namespace Shop.Application.Services.SuperSettingServices
{
    public class SuperSettingService : Service<SuperSetting, SuperSettingDto>, ISuperSettingService, IScopedDependency
    {
        public SuperSettingService(ISuperSettingRepository repository) : base(repository)
        {
        }
    }
}