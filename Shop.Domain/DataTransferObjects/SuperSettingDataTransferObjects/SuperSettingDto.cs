using System;

namespace Shop.Domain.DataTransferObjects.SuperSettingDataTransferObjects
{
    public class SuperSettingDto : BaseDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public SuperSettingTypeEnum Type { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}