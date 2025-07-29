using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Models.Products;

namespace Shop.Domain.Models.Attributes
{
    [Table("AttributeValues")]
    public class AttributeValue : BaseEntity
    {
        public string Title { get; set; }
        public int AttributeId { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(AttributeId))]
        public Attribute Attribute { get; set; }

        public List<ProductVariantAttributeValue> ProductVariantAttributeValues { get; set; }
        
        #endregion
    }
}