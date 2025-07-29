using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Domain.Models.Users
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        //public string Username { get; set; }
        //public string PasswordHash { get; set; }
        public string Code { get; set; }
        //public string PhoneNumber { get; set; }
        public string Address { get; set; }
        //public string Email { get; set; }
        //public Guid SecurityStamp { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p=> p.UserName).IsRequired().HasMaxLength(100);
        }
    }
}