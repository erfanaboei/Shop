using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Shop.Data.Context;
using Shop.Domain.Models.Roles;
using Shop.Domain.Models.Users;

namespace Shop.IOC.Configurations
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentityConfiguration(this IServiceCollection services, IdentitySetting setting)
        {
            services.AddIdentity<User, Role>(options =>
            {
                //PasswordSetting
                options.Password.RequireDigit = setting.PasswordRequireDigit;
                options.Password.RequireLowercase = setting.PasswordRequireLowercase;
                options.Password.RequireUppercase = setting.PasswordRequireUppercase;
                options.Password.RequiredLength = setting.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = setting.PasswordRequireNonAlphanumeric;
                
                //UserSetting
                options.User.RequireUniqueEmail = setting.UserRequireUniqueEmail;
            })
            .AddEntityFrameworkStores<ShopContext>()
            .AddDefaultTokenProviders();
        }
    }
}