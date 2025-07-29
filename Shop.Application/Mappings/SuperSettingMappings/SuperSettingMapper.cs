using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.SuperSettingDataTransferObjects;
using Shop.Domain.Models.SuperSettings;

namespace Shop.Application.Mappings.SuperSettingMappings
{
    public class SuperSettingMapper : GenericMapper<SuperSetting, SuperSettingDto>
    {
        public override SuperSettingDto ToDto(SuperSetting model)
        {
            return new SuperSettingDto()
            {
                Id = model.Id,
                Key = model.Key,
                Value = model.Value,
                Type = model.Type,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate
            };
        }

        public override SuperSetting ToModel(SuperSettingDto dto)
        {
            return new SuperSetting()
            {
                Id = dto.Id,
                Key = dto.Key,
                Value = dto.Value,
                Type = dto.Type,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate
            };
        }

        public override OptionDto ToOption(SuperSetting model)
        {
            throw new System.NotImplementedException();
        }
    }
}