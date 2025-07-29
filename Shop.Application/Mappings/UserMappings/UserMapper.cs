using System;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;
using Shop.Domain.Models.Users;

namespace Shop.Application.Mappings.UserMappings
{
    public class UserMapper : GenericMapper<User, UserDto>
    {
        public override UserDto ToDto(User model)
        {
            return new UserDto()
            {
                Id = model.Id,
                Name = model.Name,
                Family = model.Family,
                //PasswordHash = model.PasswordHash,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Code = model.Code,
                CreateDate = model.CreateDate,
                Email = model.Email,
                SecurityStamp = model.SecurityStamp
            };
        }

        public override User ToModel(UserDto dto)
        {
            return new User()
            {
                Name = dto.Name,
                Family = dto.Family,
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Code = dto.Code,
                CreateDate = dto.CreateDate,
                PasswordHash = SecurityHelper.GetSha256Hash(dto.Password),
                SecurityStamp = dto.SecurityStamp
            };
        }

        public override OptionDto ToOption(User model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = $"{model.Name} {model.Family}"
            };
        }
    }
}