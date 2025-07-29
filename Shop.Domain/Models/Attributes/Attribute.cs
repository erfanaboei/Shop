using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models.Attributes
{
    [Table("Attributes")]
    public class Attribute : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        #region Navigation Property

        public List<AttributeValue> AttributeValues { get; set; }

        #endregion
    }
}