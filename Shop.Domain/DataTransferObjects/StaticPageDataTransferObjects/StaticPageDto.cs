namespace Shop.Domain.DataTransferObjects.StaticPageDataTransferObjects
{
    public class StaticPageDto:BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}