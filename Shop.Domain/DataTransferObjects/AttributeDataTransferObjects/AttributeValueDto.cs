namespace Shop.Domain.DataTransferObjects.AttributeDataTransferObjects
{
    public class AttributeValueDto : BaseDto
    {
        public int AttributeId { get; set; }
        public string Title { get; set; }
    }
}