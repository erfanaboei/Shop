using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.RoleDataTransferObjects;
using Shop.Domain.Models.Roles;

namespace Shop.Application.Mappings.RoleMappings
{
    public class RoleMapper : GenericMapper<Role, RoleDto>
    {
        public override RoleDto ToDto(Role model)
        {
            return new RoleDto()
            {
                Id = model.Id,
                Name = model.Name,
                CreateDate = model.CreateDate,
                Description = model.Description,
            };
        }

        public override Role ToModel(RoleDto dto)
        {
            return new Role()
            {
                Id = dto.Id,
                Name = dto.Name,
                CreateDate = dto.CreateDate,
                Description = dto.Description,
            };
        }

        public override OptionDto ToOption(Role model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = model.Name
            };
        }
    }
}