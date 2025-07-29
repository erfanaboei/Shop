using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shop.Application.Extensions
{
    public static class ModelStageExtensions
    {
        public static List<string> GetAllErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Values
                .SelectMany(r=> r.Errors)
                .Select(r=> r.ErrorMessage)
                .ToList();
        }
    }
}