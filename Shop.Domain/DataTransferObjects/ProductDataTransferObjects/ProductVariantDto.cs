using System.Collections.Generic;

namespace Shop.Domain.DataTransferObjects.ProductDataTransferObjects
{
    public class ProductVariantDto : BaseDto
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public List<int> AttributeValueIds { get; set; }
    }
}