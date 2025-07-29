using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;

namespace Shop.Application.Mappings
{
    public abstract class GenericMapper<TModel, TDto> where TModel : class where TDto : class
    {
        public abstract TDto ToDto(TModel model);
        public abstract TModel ToModel(TDto dto);
        public abstract OptionDto ToOption(TModel model);

        public virtual List<TDto> ToDtoList(IEnumerable<TModel> models)
        {
            return models.Select(ToDto).ToList();
        }

        public virtual List<TModel> ToModelList(IEnumerable<TDto> dtoList)
        {
            return dtoList.Select(ToModel).ToList();
        }

        public virtual List<OptionDto> ToOptionList(IEnumerable<TModel> models)
        {
            return models.Select(ToOption).ToList();
        }
    }
}