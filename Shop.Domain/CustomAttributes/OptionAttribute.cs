using System;

namespace Shop.Domain.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OptionKeyAttribute : Attribute
    {
        public string PropertyName { get; }

        public OptionKeyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OptionDisplayTextAttribute : Attribute
    {
        public string Separator { get; }
        public string[] PropertyNames { get; }

        public OptionDisplayTextAttribute(params string[] propertyNames)
        {
            Separator = " - ";
            PropertyNames = propertyNames;
        }
        
        public OptionDisplayTextAttribute(string separator, params string[] propertyNames)
        {
            Separator = separator;
            PropertyNames = propertyNames;
        }
        
    }
}