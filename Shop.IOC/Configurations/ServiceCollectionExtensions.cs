using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shop.Application;
using Shop.Application.Extensions;
using Shop.Application.Mappings;
using Shop.Application.Mappings.AttributeMappings;
using Shop.Application.Mappings.CategoryMappings;
using Shop.Application.Mappings.ProductMappings;
using Shop.Application.Mappings.RoleMappings;
using Shop.Application.Mappings.SuperSettingMappings;
using Shop.Application.Mappings.UserMappings;
using Shop.Application.Utilities;
using Shop.Data.Context;
using Shop.Domain.Models.Users;
using Attribute = Shop.Domain.Models.Attributes.Attribute;

namespace Shop.IOC.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterGenericMapper(this IServiceCollection services)
        {
            var assembly = typeof(IMapper).Assembly;
            var mapperTypes = assembly.GetTypes().Where(type => typeof(IMapper).IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false});

            foreach (var mapperType in mapperTypes)
            {
                var interfaces = mapperType.GetInterfaces().Where(i => i != typeof(IMapper)).ToList();
                
                services.AddScoped(mapperType);

                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, mapperType);
                }
            }
        }
        
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }
        
        public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
                var encryptKey = Encoding.UTF8.GetBytes(jwtSetting.EncryptKey);
                
                var validationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSetting.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSetting.Issuer,
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey)
                };
                
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                            throw context.Exception;
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetService<SignInManager<User>>();
                        
                        //var userService = context.HttpContext.RequestServices.GetService<IUserService>();
                        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                        if (claimsIdentity?.Claims?.Any() != true)
                            context.Fail("No Claims Found");
                        
                        var securityStamp = claimsIdentity?.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("The Security Stamp Claim Was Not Found");

                        var validateUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validateUser == null)
                            context.Fail("The Security Stamp Is Expired");

                        // var userId = claimsIdentity.GetUserId<int>();
                        // var user = await userService.GetByIdAsync(userId, context.HttpContext.RequestAborted);

                        // if (user.SecurityStamp != Guid.Parse(securityStamp))
                        //     context.Fail("The Security Stamp Is Expired");
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw context.AuthenticateFailure;
                        
                        throw new UnauthorizedAccessException("You Are Not Authorized");
                    }
                };
            });
        }
    }
}