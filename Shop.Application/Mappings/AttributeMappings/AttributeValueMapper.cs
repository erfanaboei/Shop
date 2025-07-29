using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.Models.Attributes;

namespace Shop.Application.Mappings.AttributeMappings
{
    public class AttributeValueMapper : GenericMapper<AttributeValue, AttributeValueDto>
    {
        public override AttributeValueDto ToDto(AttributeValue model)
        {
            return new AttributeValueDto()
            {
                Id = model.Id,
                Title = model.Title,
                AttributeId = model.AttributeId,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                DeleteDate = model.DeleteDate
            };
        }

        public override AttributeValue ToModel(AttributeValueDto dto)
        {
            return new AttributeValue()
            {
                Id = dto.Id,
                Title = dto.Title,
                AttributeId = dto.AttributeId,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                DeleteDate = dto.DeleteDate
            };
        }

        public override OptionDto ToOption(AttributeValue model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = $"{model.Attribute.Title} - {model.Title}"
            };
        }
    }
}