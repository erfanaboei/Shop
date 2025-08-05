using Shop.Domain.Enums.Orders;

namespace Shop.Domain.DataTransferObjects.OrderDataTransferObjects
{
    public class OrderDto : BaseDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}