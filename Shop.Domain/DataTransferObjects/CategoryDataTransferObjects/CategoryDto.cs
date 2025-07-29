namespace Shop.Domain.DataTransferObjects.CategoryDataTransferObjects
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
    }
}