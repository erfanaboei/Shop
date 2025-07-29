using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models.Products
{
    [Table("ProductVariants")]
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public List<ProductVariantAttributeValue> ProductVariantAttributeValues { get; set; }

        #endregion
    }
}