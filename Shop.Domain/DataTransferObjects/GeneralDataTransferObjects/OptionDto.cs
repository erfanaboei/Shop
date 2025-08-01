namespace Shop.Domain.DataTransferObjects.GeneralDataTransferObjects
{
    public class OptionDto: IDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}