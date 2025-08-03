using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Shop.Domain.CustomAttributes;
using Shop.Domain.DataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.Models;

namespace Shop.Application.Mappings
{
    public static class GenericMapperExtensions
    {
        // public static TModel ToModel<TModel, TDto>(this TDto dto, TModel model = null)
        //     where TModel : class, IEntity, new()
        //     where TDto : class, IDto, new()
        // {
        //     var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
        //     return mapper.ToModel(dto, model);
        // }
        //
        // public static TDto ToDto<TModel, TDto>(this TModel model, TDto dto = null)
        //     where TModel : class, IEntity, new()
        //     where TDto : class, IDto, new()
        // {
        //     var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
        //     return mapper.ToDto(model, dto);
        // }

        public static FluentToModelMapperContext<TDto, TModel> ToModel<TModel, TDto>(this TDto dto, TModel model = null) 
            where TModel : class, IEntity, new()
            where TDto : class, IDto, new()
        {
            // var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
            // var baseModel = mapper.ToModel(dto, model);
            return new FluentToModelMapperContext<TDto, TModel>(dto, model);
        }

        public static FluentToDtoMapperContext<TModel, TDto> ToDto<TModel, TDto>(this TModel model, TDto dto = null)
            where TModel : class, IEntity, new()
            where TDto : class, IDto, new()
        {
            // var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
            // var baseDto = mapper.ToDto(model, dto);
            return new FluentToDtoMapperContext<TModel, TDto>(model, dto);
        }

        public static OptionDto ToOption<TModel>(this TModel model, Func<TModel, object> valueGetter = null, Func<TModel, object> textGetter = null)
            where TModel : class, IEntity, new()
        {
            var type = typeof(TModel);
            string value;
            string text;
            
            if (valueGetter != null)
            {
                value = valueGetter(model)?.ToString();
            }
            else
            {
                var valueAttr = type.GetCustomAttribute<OptionKeyAttribute>();
                if (valueAttr == null)
                    throw new InvalidOperationException($"no [OptionValue] attribute found on {type.Name}");

                var valueProp = type.GetProperty(valueAttr.PropertyName);
                if (valueProp == null)
                    throw new InvalidOperationException($"property '{valueAttr.PropertyName}' not found on {type.Name}");
                
                value = valueProp.GetValue(model)?.ToString();
            }

            if (textGetter != null)
            {
                text = textGetter(model)?.ToString();
            }
            else
            {
                var textAttr = type.GetCustomAttribute<OptionDisplayTextAttribute>();
                if (textAttr == null)
                    throw new InvalidOperationException($"no [OptionText] attribute found on {type.Name}");
                
                var separator = string.IsNullOrEmpty(textAttr.Separator) ? " - " : textAttr.Separator;
                var textParts = textAttr.PropertyNames.Select(name =>
                {
                    var prop = type.GetProperty(name);
                    if (prop == null)
                        throw new InvalidOperationException($"Property '{name}' not found in {type.Name}");
                    
                    return prop.GetValue(model)?.ToString();
                })
                .Where(r=> !string.IsNullOrWhiteSpace(r));
                
                text = string.Join(separator, textParts);
            }

            return new OptionDto()
            {
                Value = value ?? string.Empty,
                Text = text ?? string.Empty,
            };
        }

        public static List<OptionDto> ToOptions<TModel>(this List<TModel> models, Func<TModel, object> valueGetter = null, Func<TModel, object> textGetter = null)
            where TModel : class, IEntity, new()
        {
            return models.Select(r => ToOption(r, valueGetter, textGetter)).ToList();
        }
    }
}