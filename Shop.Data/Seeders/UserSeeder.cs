using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Application.Utilities;
using Shop.Domain.Models.Users;
namespace Shop.Data.Seeders
{
    public class UserSeeder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "سوپرادمین",
                    UserName = "SuperAdmin",
                    PasswordHash = SecurityHelper.GetSha256Hash("Super@dmin"),
                    CreateDate = new DateTime(2025, 07, 10, 7, 46, 0, DateTimeKind.Local),
                    Code = "1",
                },
                new User()
                {
                    Id = 2,
                    Name = "ادمین",
                    UserName = "admin",
                    PasswordHash = SecurityHelper.GetSha256Hash("1234"),
                    CreateDate = new DateTime(2025, 07, 10, 7, 46, 0, DateTimeKind.Local),
                    Code = "2",
                }
            });
        }
    }
}