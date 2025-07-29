using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Models.Attributes;

namespace Shop.Domain.Models.Products
{
    [Table("ProductVariantAttributeValues")]
    public class ProductVariantAttributeValue : BasePureEntity
    {
        public int ProductVariantId { get; set; }
        public int AttributeValueId { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(ProductVariantId))]
        public ProductVariant ProductVariant { get; set; }
        
        [ForeignKey(nameof(AttributeValueId))]
        public AttributeValue AttributeValue { get; set; }

        #endregion
    }
}