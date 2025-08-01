using System;
using System.Reflection;

namespace Shop.Application.Mappings
{
    public class MappingRule<TSource, TTarget> where TSource : class where TTarget : class
    {
        public PropertyInfo TargetProperty { get; set; }
        public Func<TSource, object> ValueGetter { get; set; }
    }
}