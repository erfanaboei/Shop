using Shop.Application.IServices.IAttributeServices;
using Shop.Application.Mappings.AttributeMappings;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;
using Shop.Domain.IRepositories.IAttributeRepositories;
using Shop.Domain.Models.Attributes;

namespace Shop.Application.Services.AttributeServices
{
    public class AttributeValueService : Service<AttributeValue, AttributeValueDto>, IAttributeValueService, IScopedDependency
    {
        public AttributeValueService(IAttributeValueRepository repository, AttributeValueMapper mapper) : base(repository, mapper)
        {
        }
    }
}