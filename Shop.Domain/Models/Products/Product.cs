using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models.Products
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public int Inventory { get; set; }

        #region Navigation Property

        public List<ProductImage> ProductImages { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }

        #endregion
    }
}