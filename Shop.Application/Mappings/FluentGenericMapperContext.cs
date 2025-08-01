using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Shop.Application.Mappings
{
    public class FluentGenericMapperContext<TSource, TTarget> where TSource : class where TTarget : class
    {
        private readonly TSource _source;
        private readonly TTarget _target;

        private readonly List<MappingRule<TSource, TTarget>> _rules = new();

        public FluentGenericMapperContext(TSource source, TTarget target)
        {
            _source = source;
            _target = target;
        }

        public FluentGenericMapperContext<TSource, TTarget> ForMember<TMember>(Expression<Func<TTarget, TMember>> destinationMember, Func<TSource, object> valueGetter)
        {
            if (destinationMember.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            {
                _rules.Add(new MappingRule<TSource, TTarget>()
                {
                    TargetProperty = propertyInfo,
                    ValueGetter = valueGetter
                });
            }

            return this;
        }

        public TTarget Map()
        {
            foreach (var rule in _rules)
            {
                var value = rule.ValueGetter(_source);
                rule.TargetProperty.SetValue(_target, value);
            }
            
            return _target;
        }
    }
}