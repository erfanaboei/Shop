namespace Shop.Domain.DataTransferObjects.OrderDataTransferObjects
{
    public class OrderItemDto : BasePureDto
    {
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}