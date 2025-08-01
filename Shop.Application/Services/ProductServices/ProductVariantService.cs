using Shop.Application.IServices.IProductServices;
using Shop.Application.Mappings;
using Shop.Application.Mappings.ProductMappings;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Application.Services.ProductServices
{
    public class ProductVariantService : Service<ProductVariant, ProductVariantDto>, IProductVariantService, IScopedDependency
    {
        public ProductVariantService(IProductVariantRepository repository) : base(repository)
        {
        }
    }
}