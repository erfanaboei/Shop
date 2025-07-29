using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.DataTransferObjects.SuperSettingDataTransferObjects;

namespace Shop.Domain.Models.SuperSettings
{
    [Table("SuperSettings")]
    public class SuperSetting : BasePureEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public SuperSettingTypeEnum Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}