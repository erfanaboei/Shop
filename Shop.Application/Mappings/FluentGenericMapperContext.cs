using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Shop.Domain.DataTransferObjects;
using Shop.Domain.Models;

namespace Shop.Application.Mappings
{
    public class FluentToDtoMapperContext<TModel, TDto> 
        where TModel : class, IEntity, new() 
        where TDto : class, IDto, new()
    {
        private readonly TModel _model;
        private readonly TDto _dto;

        private readonly List<MappingRule<TModel, TDto>> _rules = new();
        private readonly List<(string propName, IgnoreMode mode)> _ignoredProperties = new();

        public FluentToDtoMapperContext(TModel model, TDto dto)
        {
            _model = model;
            _dto = dto;
        }

        public FluentToDtoMapperContext<TModel, TDto> ForMember<TMember>(Expression<Func<TDto, TMember>> destinationMember, Func<TModel, object> valueGetter)
        {
            if (destinationMember.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            {
                _rules.Add(new MappingRule<TModel, TDto>()
                {
                    TargetProperty = propertyInfo,
                    ValueGetter = valueGetter
                });
            }

            return this;
        }

        public FluentToDtoMapperContext<TModel, TDto> Ignore<TMember>(Expression<Func<TDto, TMember>> destinationMember, IgnoreMode ignoreMode = IgnoreMode.Skip)
        {
            if (destinationMember.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            {
                _ignoredProperties.Add((propertyInfo.Name, ignoreMode));
            }

            return this;
        }
        
        public FluentToDtoMapperContext<TModel, TDto> Ignore(string propertyName, IgnoreMode ignoreMode = IgnoreMode.Skip)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                _ignoredProperties.Add((propertyName, ignoreMode));

            return this;
        }
        
        public TDto Map()
        {
            var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
            
            var skipProps = _ignoredProperties.Where(x => x.mode == IgnoreMode.Skip).Select(r=> r.propName).ToList();

            var dto = mapper.ToDto(_model, _dto, skipProps);

            ApplyIgnoreRules(dto);
            ApplyMappingRules(dto);
            
            return dto;
        }

        private void ApplyIgnoreRules(TDto dto)
        {
            foreach (var prop in typeof(TDto).GetProperties())
            {
                var ignore = _ignoredProperties.FirstOrDefault(x => x.propName == prop.Name);
                
                if (ignore == default) continue;

                if (ignore.mode == IgnoreMode.Nullify)
                    prop.SetValue(dto, null);
            }
        }
        
        private void ApplyMappingRules(TDto dto)
        {
            foreach (var rule in _rules)
            {
                if (_ignoredProperties.Any(r=> r.propName == rule.TargetProperty.Name)) continue;
                
                var value = rule.ValueGetter(_model);
                rule.TargetProperty.SetValue(dto, value);
            }
        }
    }
    
    public class FluentToModelMapperContext<TDto, TModel> 
        where TDto : class, IDto, new() 
        where TModel : class, IEntity, new()
    {
        
        private readonly TDto _dto;
        private readonly TModel _model;

        private readonly List<MappingRule<TDto, TModel>> _rules = new();
        private readonly List<(string propName, IgnoreMode mode)> _ignoredProperties = new();

        public FluentToModelMapperContext(TDto dto, TModel model)
        {
            _dto = dto;
            _model = model;
        }

        public FluentToModelMapperContext<TDto, TModel> ForMember<TMember>(Expression<Func<TModel, TMember>> destinationMember, Func<TDto, object> valueGetter)
        {
            if (destinationMember.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            {
                _rules.Add(new MappingRule<TDto, TModel>()
                {
                    TargetProperty = propertyInfo,
                    ValueGetter = valueGetter
                });
            }

            return this;
        }

        public FluentToModelMapperContext<TDto, TModel> Ignore<TMember>(Expression<Func<TModel, TMember>> destinationMember, IgnoreMode ignoreMode = IgnoreMode.Skip)
        {
            if (destinationMember.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            {
                _ignoredProperties.Add((propertyInfo.Name, ignoreMode));
            }

            return this;
        }
        
        public FluentToModelMapperContext<TDto, TModel> Ignore(string propertyName, IgnoreMode ignoreMode = IgnoreMode.Skip)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                _ignoredProperties.Add((propertyName, ignoreMode));

            return this;
        }
        
        public TModel Map()
        {
            var mapper = GenericMapperRegistry.GetMapper<TModel, TDto>();
            
            var skipProps = _ignoredProperties.Where(x => x.mode == IgnoreMode.Skip).Select(r=> r.propName).ToList();
            
            var model = mapper.ToModel(_dto, _model, skipProps);

            ApplyIgnoreRules(model);
            ApplyMappingRules(model);
            
            return model;
        }

        private void ApplyIgnoreRules(TModel model)
        {
            foreach (var prop in typeof(TModel).GetProperties())
            {
                var ignore = _ignoredProperties.FirstOrDefault(x => x.propName == prop.Name);
                
                if (ignore == default) continue;

                if (ignore.mode == IgnoreMode.Nullify)
                    prop.SetValue(model, null);
            }
        }
        
        private void ApplyMappingRules(TModel model)
        {
            foreach (var rule in _rules)
            {
                if (_ignoredProperties.Any(r=> r.propName == rule.TargetProperty.Name)) continue;
                
                var value = rule.ValueGetter(_dto);
                rule.TargetProperty.SetValue(model, value);
            }
        }
    }
}