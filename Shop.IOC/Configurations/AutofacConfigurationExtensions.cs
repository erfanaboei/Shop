using System;
using Autofac;
using Shop.Application.IServices;
using Shop.Application.Mappings;
using Shop.Application.Services;
using Shop.Data.Repositories;
using Shop.Domain;
using Shop.Domain.IRepositories;
using Shop.Domain.Models;

namespace Shop.IOC.Configurations
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(Service<,>)).As(typeof(IService<,>)).InstancePerLifetimeScope();

            // containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            // containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            // containerBuilder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            
            var applicationAssembly = typeof(IService<,>).Assembly;
            var dataAssembly = typeof(Repository<>).Assembly;
            var domainAssembly = typeof(IEntity).Assembly;
            
            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dataAssembly, domainAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dataAssembly, domainAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            
            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dataAssembly, domainAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(applicationAssembly)
                .Where(r=> r.IsSubclassOfGeneric(typeof(GenericMapper<,>)))
                .AsSelf()
                .As(r => new[] { r.BaseType })
                .InstancePerLifetimeScope();
        }

        private static bool IsSubclassOfGeneric(this Type type, Type generic)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (generic == cur)
                    return true;
                type = type.BaseType;
            }

            return false;
        }
    }
}