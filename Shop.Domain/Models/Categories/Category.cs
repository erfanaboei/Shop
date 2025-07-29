using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Models.Products;

namespace Shop.Domain.Models.Categories
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        
        #region Navigation Property

        public Category Parent { get; set; }

        public List<Category> Children { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

        #endregion
    }
}