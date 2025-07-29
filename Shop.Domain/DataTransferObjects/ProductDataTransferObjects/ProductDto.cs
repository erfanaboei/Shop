using System.Collections.Generic;

namespace Shop.Domain.DataTransferObjects.ProductDataTransferObjects
{
    public class ProductDto : BaseDto
    {
        public string Title { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? DiscountPercentage { get; set; }
        public int Inventory { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}