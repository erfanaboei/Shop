using Shop.Application.IServices.IAttributeServices;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.AttributeDataTransferObjects;
using Shop.Domain.IRepositories.IAttributeRepositories;
using Shop.Domain.Models.Attributes;

namespace Shop.Application.Services.AttributeServices
{
    public class AttributeService : Service<Attribute, AttributeDto>, IAttributeService, IScopedDependency
    {
        public AttributeService(IAttributeRepository repository) : base(repository)
        {
        }
    }
}