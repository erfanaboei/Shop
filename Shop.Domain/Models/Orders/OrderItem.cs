using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Models.Products;

namespace Shop.Domain.Models.Orders
{
    [Table("OrderItems")]
    public class OrderItem : BasePureEntity
    {
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        
        [ForeignKey(nameof(ProductVariantId))]
        public ProductVariant ProductVariant { get; set; }

        #endregion
    }
}