using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;
using Shop.Domain.Models.Attributes;

namespace Shop.Application.IServices.IAttributeServices
{
    public interface IAttributeService : IService<Attribute, AttributeDto>
    {
        
    }
}