using System;
using System.Collections.Generic;
using Shop.Application.Mappings.AttributeMappings;
using Shop.Application.Mappings.CategoryMappings;
using Shop.Application.Mappings.ProductMappings;
using Shop.Application.Mappings.RoleMappings;
using Shop.Application.Mappings.SuperSettingMappings;
using Shop.Application.Mappings.UserMappings;
using Shop.Domain.DataTransferObjects;
using Shop.Domain.Models;

namespace Shop.Application.Mappings
{
    public static class GenericMapperRegistry
    {
        private static readonly Dictionary<(Type SourceType, Type TargetType), object> Mappers = new();

        public static void RegisterAll()
        {
            Register(new UserMapper());
            Register(new AttributeMapper());
            Register(new CategoryMapper());
            Register(new ProductMapper());
            Register(new ProductVariantMapper());
            Register(new SuperSettingMapper());
            Register(new RoleMapper());
            Register(new AttributeValueMapper());
        }
        
        private static void Register<TModel, TDto>(GenericMapper<TModel, TDto> mapper) 
            where TModel : class, IEntity, new()
            where TDto : class, IDto, new()
        {
            Mappers[(typeof(TModel), typeof(TDto))] = mapper;
            Mappers[(typeof(TDto), typeof(TModel))] = mapper;
        }

        public static GenericMapper<TModel, TDto> GetMapper<TModel, TDto>()
            where TModel : class, IEntity, new()
            where TDto : class, IDto, new()
        {
            var key = (typeof(TModel), typeof(TDto));

            if (Mappers.TryGetValue(key, out var mapper))
                return (GenericMapper<TModel, TDto>)mapper;
            
            throw new InvalidOperationException($"Mapper for {typeof(TModel).Name} <-> {typeof(TDto).Name} is not registered.");
        }
    }
}