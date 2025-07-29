using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models.Products
{
    [Table("ProductImages")]
    public class ProductImage : BasePureEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        #endregion
    }
}