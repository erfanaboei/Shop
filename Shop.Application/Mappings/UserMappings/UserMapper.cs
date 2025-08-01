using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;
using Shop.Domain.Models.Users;

namespace Shop.Application.Mappings.UserMappings
{
    public class UserMapper : GenericMapper<User, UserDto>
    {
        public override User ToModel(UserDto dto, User model = null)
        {
            if (dto.Password != null)
            {
                dto.PasswordHash = SecurityHelper.GetSha256Hash(dto.Password);
            }
            return base.ToModel(dto, model);
        }
    }
}