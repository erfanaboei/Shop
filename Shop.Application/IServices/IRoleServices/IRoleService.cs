using Shop.Domain.DataTransferObjects.RoleDataTransferObjects;
using Shop.Domain.Models.Roles;

namespace Shop.Application.IServices.IRoleServices
{
    public interface IRoleService : IService<Role, RoleDto>
    {
    }
}