using Shop.Application.IServices.IRoleServices;
using Shop.Application.Mappings.RoleMappings;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.RoleDataTransferObjects;
using Shop.Domain.IRepositories.IRoleRepositories;
using Shop.Domain.Models.Roles;

namespace Shop.Application.Services.RoleServices
{
    public class RoleService : Service<Role, RoleDto>, IRoleService, IScopedDependency
    {
        public RoleService(IRoleRepository repository) : base(repository)
        {
        }
    }
}