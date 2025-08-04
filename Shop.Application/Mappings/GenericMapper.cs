using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shop.Domain.DataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.Models;

namespace Shop.Application.Mappings
{
    public interface IMapper
    {
        
    }
    
    public abstract class GenericMapper<TModel, TDto> : IMapper where TModel : class, IEntity, new() where TDto : class, IDto, new()
    {
        private static readonly ConcurrentDictionary<(Type SourceType, Type TargetType), List<(PropertyInfo SourceProp, PropertyInfo TargetProp)>> _propertyMapCache = new();
        
        public virtual TDto ToDto(TModel model, TDto dto = null, IEnumerable<string> ignoreProperties = null)
        {
            if (model == null) return null;
            
            var result = dto ?? Activator.CreateInstance<TDto>();
            var key = (typeof(TModel), typeof(TDto));

            if (!_propertyMapCache.TryGetValue(key, out var propertyMap))
            {
                
                propertyMap = (
                    from dtoProp in typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    join modelProp in typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    on dtoProp.Name equals modelProp.Name
                    where dtoProp.CanWrite && (dtoProp.PropertyType == modelProp.PropertyType ||
                                               Nullable.GetUnderlyingType(dtoProp.PropertyType) == modelProp.PropertyType ||
                                               Nullable.GetUnderlyingType(modelProp.PropertyType) == dtoProp.PropertyType)
                    select (modelProp, dtoProp)
                ).ToList();
                
                _propertyMapCache[key] = propertyMap;
            }

            var finalMap = propertyMap.ToList();

            if (ignoreProperties != null && ignoreProperties.Any())
            {
                finalMap = propertyMap.Where(r => !ignoreProperties.Contains(r.SourceProp.Name)).ToList();
            }
            
            foreach (var (sourceProp, targetProp) in finalMap)
            {
                var value = sourceProp.GetValue(model);
                targetProp.SetValue(result, value);
            }
            
            return result;
        }

        public virtual TModel ToModel(TDto dto, TModel model = null, IEnumerable<string> ignoreProperties = null)
        {
            if (dto == null) return null;
            
            var result = model ?? Activator.CreateInstance<TModel>();
            var key = (typeof(TDto), typeof(TModel));

            if (!_propertyMapCache.TryGetValue(key, out var propertyMap))
            {
                propertyMap = (
                    from dtoProp in typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    join modelProp in typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    on dtoProp.Name equals modelProp.Name
                    where modelProp.CanWrite && (dtoProp.PropertyType == modelProp.PropertyType ||
                                                 Nullable.GetUnderlyingType(dtoProp.PropertyType) == modelProp.PropertyType ||
                                                 Nullable.GetUnderlyingType(modelProp.PropertyType) == dtoProp.PropertyType)
                    select (dtoProp, modelProp)
                ).ToList();
                
                _propertyMapCache[key] = propertyMap;
            }

            var finalMap = propertyMap.ToList();

            if (ignoreProperties != null && ignoreProperties.Any())
            {
                finalMap = propertyMap.Where(r => !ignoreProperties.Contains(r.SourceProp.Name)).ToList();
            }
            
            foreach (var (sourceProp, targetProp) in finalMap)
            {
                var value = sourceProp.GetValue(dto);
                targetProp.SetValue(result, value);
            }
            
            return result;
        }

        public virtual List<TDto> ToDtoList(IEnumerable<TModel> models)
        {
            return models == null ? new List<TDto>() : models.Select(r=> ToDto(r)).ToList();
        }

        public virtual List<TModel> ToModelList(IEnumerable<TDto> dtoList)
        {
            return dtoList == null ? new List<TModel>() : dtoList.Select(r => ToModel(r)).ToList();
        }
    }
}