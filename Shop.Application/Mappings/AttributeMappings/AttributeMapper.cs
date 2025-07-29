using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.Models.Attributes;

namespace Shop.Application.Mappings.AttributeMappings
{
    public class AttributeMapper : GenericMapper<Attribute, AttributeDto>
    {
        public override AttributeDto ToDto(Attribute model)
        {
            return new AttributeDto()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                DeleteDate = model.DeleteDate
            };
        }

        public override Attribute ToModel(AttributeDto dto)
        {
            return new Attribute()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                DeleteDate = dto.DeleteDate
            };
        }

        public override OptionDto ToOption(Attribute model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = model.Title
            };
        }
    }
}