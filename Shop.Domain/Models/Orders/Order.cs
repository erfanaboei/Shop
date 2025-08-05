using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Enums.Orders;
using Shop.Domain.Models.Users;

namespace Shop.Domain.Models.Orders
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatusEnum Status { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        
        #endregion
    }
}