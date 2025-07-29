using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Utilities;

namespace Shop.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Services
            // services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IJwtService, JwtService>();

            //Repositories
            // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // services.AddScoped<IUserRepository, UserRepository>();
            
            //Mappers
            // services.AddScoped<GenericMapper<User, UserDto>, UserMapper>();
            // services.AddScoped<GenericMapper<SuperSetting, SuperSettingDto>, SuperSettingMapper>();
            // services.AddScoped<GenericMapper<Role, RoleDto>, RoleMapper>();
            // services.AddScoped<GenericMapper<Product, ProductDto>, ProductMapper>();
            
            //Other
            services.AddScoped<RequestResultFilterAttribute>();
        }
    }
}