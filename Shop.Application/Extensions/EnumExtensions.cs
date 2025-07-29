using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Shop.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            try
            {
                var attribute = value?.GetType().GetField(value.ToString())?
                    .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

                if (attribute == null)
                    return value?.ToString();
                
                var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);
                return propValue?.ToString();
            }
            catch (Exception e)
            {
                return "-";
            }
        }
        
        public enum DisplayProperty
        {
            Description,
            GroupName,
            Name,
            Prompt,
            ShortName,
            Order
        }
    }
}