using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Models.Roles;

namespace Shop.Data.Seeders
{
    public class RoleSeeder : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = "SuperAdmin",
                    CreateDate = new DateTime(2025, 07, 10, 7, 46, 0, DateTimeKind.Local),
                    Description = "SuperAdmin",
                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin",
                    CreateDate = new DateTime(2025, 07, 10, 7, 46, 0, DateTimeKind.Local),
                    Description = "Admin role",
                },
                new Role()
                {
                    Id = 3,
                    Name = "Customer",
                    CreateDate = new DateTime(2025, 07, 10, 7, 46, 0, DateTimeKind.Local),
                    Description = "Customer",
                }
            });
        }
    }
}