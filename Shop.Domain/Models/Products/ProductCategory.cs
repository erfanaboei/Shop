using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Models.Categories;

namespace Shop.Domain.Models.Products
{
    [Table("ProductCategories")]
    public class ProductCategory : BasePureEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        #endregion
    }
}