using System.Threading;
using System.Threading.Tasks;
using Shop.Application.IServices.IProductServices;
using Shop.Application.Mappings.ProductMappings;
using Shop.Application.Utilities;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;
using Shop.Domain.IRepositories.IProductRepositories;
using Shop.Domain.Models.Products;

namespace Shop.Application.Services.ProductServices
{
    public class ProductService : Service<Product, ProductDto>, IProductService, IScopedDependency
    {
        public ProductService(IProductRepository repository, ProductMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<RequestResult<ProductDto>> AddAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            if (await IsExistAsync(r => r.Code == dto.Code, cancellationToken))
                return new RequestResult<ProductDto>(false, RequestResultStatusCode.BadRequest, null, "کد وارد شده تکراری است!");
            
            return await base.AddAsync(dto, cancellationToken);
        }
    }
}